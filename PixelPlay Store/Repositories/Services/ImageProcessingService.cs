using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public class ImageProcessingService : IImageProcessingService
{
    public async Task ResizeAndSaveAsync(Stream inputStream, string savePath, int width, int height)
    {
        using var image = await Image.LoadAsync(inputStream);

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(width, height),
            Mode = ResizeMode.Crop // Crop to fill without distortion
        }));

        await image.SaveAsync(savePath);
    }
}

