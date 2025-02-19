using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Repositories.Repos
{
    public class GameForm : IGameForm
    {
        private readonly MyDbContext context;
        public GameForm(MyDbContext myDbContext)
        {
            context = myDbContext;
        }

        public List<SelectListItem> GetCategoriesData()
        {
            return context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .ToList();
        }

        public List<SelectListItem> GetDevicesData()
        {
            return context.Devices
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .ToList();
        }
    }
}
