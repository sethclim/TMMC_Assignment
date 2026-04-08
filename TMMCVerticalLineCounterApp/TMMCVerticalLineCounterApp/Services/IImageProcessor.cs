using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    public interface IImageProcessor
    {
        public int Process(ImageData image);
    }
}

