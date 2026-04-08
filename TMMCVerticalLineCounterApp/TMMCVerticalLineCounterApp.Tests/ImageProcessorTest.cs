using TMMCVerticalLineCounterApp.Models;
using TMMCVerticalLineCounterApp.Services;
using Moq;
using Microsoft.Extensions.Logging;

namespace TMMCVerticalLineCounterApp.Tests
{
    [TestFixture]
    public class ImageProcessorTests
    {
        private IImageProcessor _processor;
        
        [SetUp]
        public void Setup()
        {
            var loggerMock = new Mock<ILogger<ImageProcessor>>();

            var factoryMock = new Mock<ILoggerFactory>();
            factoryMock
                .Setup(f => f.CreateLogger(It.IsAny<string>()))
                .Returns(loggerMock.Object);
            _processor = new ImageProcessor(factoryMock.Object);
        }

        [Test]
        public void CountVerticalLines_ShouldReturnCorrectCount_ForSimpleImage()
        {
            var pixels = new byte[]
            {
                0,0,0,255, 255,255,255,255, 0,0,0,255, // simple 3x1 image
            };
            var image = new ImageData
            {
                Width = 3,
                Height = 1,
                Pixels = pixels
            };

            int lineCount = _processor.Process(image);

            Assert.That(lineCount == 2, "The processor should detect 2 vertical black lines.");
        }

        [Test]
        public void Process_ShouldThrow_ForEmptyImage()
        {
            var image = new ImageData { Width = 0, Height = 0, Pixels = Array.Empty<byte>() };
            Assert.Throws<ArgumentException>(() => _processor.Process(image));
        }
    }
}