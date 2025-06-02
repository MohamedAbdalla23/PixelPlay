namespace PixelPlay.Repositories.ReposInterface
{
    public interface IGameForm
    {
        Task<string> SaveCover(IFormFile file);

        Task<string> SaveCoverWithResizeAsync(IFormFile file, int width, int height);
    }
}
