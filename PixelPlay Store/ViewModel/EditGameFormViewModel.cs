namespace PixelPlay.ViewModel
{
    public class EditGameFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxImageSizeinBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
