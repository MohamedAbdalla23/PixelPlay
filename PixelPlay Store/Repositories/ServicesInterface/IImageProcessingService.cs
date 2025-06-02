namespace PixelPlay.Repositories.ServicesInterface
{
    public interface IImageProcessingService
    {
        Task ResizeAndSaveAsync(Stream inputStream, string savePath, int width, int height);
    }
}
