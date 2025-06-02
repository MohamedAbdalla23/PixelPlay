using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Repositories.Repos
{
    public class GameForm : IGameForm
    {
        private readonly IWebHostEnvironment webhostenvironment;
        private readonly string imagepath;
        private readonly IImageProcessingService _imageProcessingService;

        public GameForm(IWebHostEnvironment webHostenvironment, IImageProcessingService imageProcessingService)
        {
            webhostenvironment = webHostenvironment;
            _imageProcessingService = imageProcessingService;
            imagepath = $"{webhostenvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task<string> SaveCover(IFormFile file)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(imagepath, covername);
            using var stream = File.Create(path);
            await file.CopyToAsync(stream);
            return covername;
        }

        public async Task<string> SaveCoverWithResizeAsync(IFormFile file, int width, int height)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(imagepath, covername);

            await _imageProcessingService.ResizeAndSaveAsync(file.OpenReadStream(), path, width, height);

            return covername;
        }
    }
}
