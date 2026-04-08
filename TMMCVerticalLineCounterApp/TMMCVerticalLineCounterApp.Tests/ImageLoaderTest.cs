using TMMCVerticalLineCounterApp.Models;
using TMMCVerticalLineCounterApp.Services;
using Moq;
using Microsoft.Extensions.Logging;

namespace TMMCVerticalLineCounterApp.Tests
{
    [TestFixture]
    public class ImageLoaderTests
    {
        private IImageLoader _loader;
        
        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<JpegImageLoader>>();

            var factoryMock = new Mock<ILoggerFactory>();
            factoryMock
                .Setup(f => f.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);

            _loader = new JpegImageLoader(factoryMock.Object);
        }

        [Test]
        public void Load_ShouldReturnImageData_ForValidJpg()
        {
            var imagePath = Path.Combine(AppContext.BaseDirectory, "img_1.jpg");
            Console.WriteLine(imagePath);
            var imageData = _loader.Load(imagePath);

            Assert.That(imageData.Width, Is.EqualTo(200));
            Assert.That(imageData.Height, Is.EqualTo(200));
            Assert.That(imageData.Pixels.Length, Is.EqualTo(200 * 200 * 4));
        }
    }
}