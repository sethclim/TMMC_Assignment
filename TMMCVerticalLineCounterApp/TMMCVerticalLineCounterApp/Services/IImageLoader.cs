using TMMCVerticalLineCounterApp.Models;

namespace TMMCVerticalLineCounterApp.Services
{
    public interface IImageLoader
    {
        ImageData Load(string path);
    }
}
