using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TMMCVerticalLineCounterApp;
using TMMCVerticalLineCounterApp.Services;
using TMMCVerticalLineCounterApp.Models;
using System;
using System.Threading.Tasks;
using System.IO;

namespace TMMCVerticalLineCounterApp.Tests
{
    [TestFixture]
    public class AppRunTests
    {
        private Mock<ILogger<App>> _loggerMock = null!;
        private Mock<IImageLoader> _loaderMock = null!;
        private Mock<IImageProcessor> _processorMock = null!;
        private Mock<IConfiguration> _configMock = null!;
        private App _app = null!;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<App>>();
            _configMock = new Mock<IConfiguration>();
            _loaderMock = new Mock<IImageLoader>();
            _processorMock = new Mock<IImageProcessor>();

            _app = new App(_loggerMock.Object, _configMock.Object, _loaderMock.Object, _processorMock.Object);
        }

        [Test]
        public void Run_ShouldThrow_WhenFileNameMissing()
        {
            _configMock.Setup(c => c["fileName"]).Returns<string?>(null);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _app.Run());
            Assert.That(ex!.Message, Is.EqualTo("fileName is required"));
        }

        [Test]
        public async Task Run_ShouldLogError_WhenFileNameIsRelative()
        {
            string relativePath = "relative/path.jpg";
            _configMock.Setup(c => c["fileName"]).Returns(relativePath);

            await _app.Run();

            _loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("must be absolute")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Test]
        public async Task Run_ShouldCallLoaderAndProcessor_WhenFileNameIsAbsolute()
        {
            string absPath = Path.Combine(AppContext.BaseDirectory,  "img_1.jpg");
            _configMock.Setup(c => c["fileName"]).Returns(absPath);

            var dummyImage = new ImageData { Width = 1, Height = 1, Pixels = new byte[4] };
            _loaderMock.Setup(l => l.Load(absPath)).Returns(dummyImage);
            _processorMock.Setup(p => p.Process(dummyImage)).Returns(5);

           
            int result = await _app.Run();
            Assert.That(result, Is.EqualTo(5));

            _loaderMock.Verify(l => l.Load(absPath), Times.Once);
            _processorMock.Verify(p => p.Process(dummyImage), Times.Once);

        }

        [Test]
        public async Task Run_ShouldLogNumberOfLines_WhenProcessingSucceeds()
        {
            string absPath = Path.Combine(AppContext.BaseDirectory, "img_1.jpg");
            _configMock.Setup(c => c["fileName"]).Returns(absPath);

            var dummyImage = new ImageData { Width = 1, Height = 1, Pixels = new byte[4] };
            _loaderMock.Setup(l => l.Load(absPath)).Returns(dummyImage);
            _processorMock.Setup(p => p.Process(dummyImage)).Returns(7);

            int result = await _app.Run();

            Assert.That(result, Is.EqualTo(7));
        }
    }
}