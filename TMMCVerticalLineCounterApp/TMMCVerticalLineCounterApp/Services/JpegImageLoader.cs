using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    internal class JpegImageLoader : IImageLoader
    {
        public ImageData Load(string path)
        {
            using var image = Image.Load<Rgba32>(path);

            var pixels = new byte[image.Width * image.Height * 4];
            image.CopyPixelDataTo(pixels);

            return new ImageData
            {
                Width = image.Width,
                Height = image.Height,
                Pixels = pixels
            };
        }
    }
}
