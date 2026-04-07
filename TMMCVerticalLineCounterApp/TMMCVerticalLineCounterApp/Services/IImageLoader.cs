using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    internal interface IImageLoader
    {
        ImageData Load(string path);
    }
}
