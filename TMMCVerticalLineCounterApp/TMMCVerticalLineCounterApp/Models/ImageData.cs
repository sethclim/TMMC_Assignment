namespace TMMCVerticalLineCounterApp.Models
{
    internal class ImageData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public required byte[] Pixels { get; set; }
    }
}

