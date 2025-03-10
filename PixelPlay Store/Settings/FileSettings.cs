
namespace PixelPlay.Settings
{
    public static class FileSettings
    {
        public const string ImagesPath = "/assets/images";

        public const string AllowedExtensions = ".JPG,.JPEG,.PNG";

        public const int MaxImageSizeinMB = 3;
        public const int MaxImageSizeinBytes = MaxImageSizeinMB * 1024 * 1024;
    }
}
