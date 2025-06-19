namespace PixelPlay.ViewModel
{
    public class CreateGameFormViewModel : GameFormViewModel
    {
        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxImageSizeinBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
