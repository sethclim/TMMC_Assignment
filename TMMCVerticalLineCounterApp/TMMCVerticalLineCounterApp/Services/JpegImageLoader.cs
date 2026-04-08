using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    public class JpegImageLoader(ILoggerFactory loggerFactory) : IImageLoader
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger(nameof(JpegImageLoader));

        /// <summary>
        /// Loads an Image from a provided path and return metadata + pixels
        /// </summary>
        /// <param name="path">Absolute path to an image file</param>
        /// <returns>ImageData containing Width, Height, and RGBA pixels</returns>
        public ImageData Load(string path)
        {
            _logger.LogInformation($"Loading {path}...");
            using var image = Image.Load<Rgba32>(path);

            var pixels = new byte[image.Width * image.Height * 4];
            image.CopyPixelDataTo(pixels);

            _logger.LogInformation($"imageData length: {pixels.Length}");

            return new ImageData
            {
                Width = image.Width,
                Height = image.Height,
                Pixels = pixels
            };
        }
    }
}
