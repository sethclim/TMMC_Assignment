using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    internal interface IImageProcessor
    {
        int Process(ImageData image);
    }
}

