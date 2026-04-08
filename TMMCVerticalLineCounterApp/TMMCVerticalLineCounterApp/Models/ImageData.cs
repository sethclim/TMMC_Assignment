namespace TMMCVerticalLineCounterApp.Models
{
    /// <summary>
    /// Represents raw image data including width, height, and RGBA pixels.
    /// </summary>
    public class ImageData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public required byte[] Pixels { get; set; }
    }
}

