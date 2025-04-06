using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Repositories.Repos
{
    public class GameForm : IGameForm
    {
        //private readonly MyDbContext context;
        private readonly IWebHostEnvironment webhostenvironment;
        private readonly string imagepath;
        public GameForm(/*MyDbContext myDbContext,*/ IWebHostEnvironment webHostenvironment)
        {
            //context = myDbContext;
            webhostenvironment = webHostenvironment;
            imagepath = $"{webhostenvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        //The Old Implementation
        //************************************

        //public List<SelectListItem> GetCategoriesData()
        //{
        //    return context.Categories
        //        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
        //        .OrderBy(c => c.Text)
        //        .AsNoTracking()
        //        .ToList();
        //}

        //public List<SelectListItem> GetDevicesData()
        //{
        //    return context.Devices
        //        .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
        //        .OrderBy(d => d.Text)
        //        .AsNoTracking()
        //        .ToList();
        //}       

        public async Task<string> SaveCover(IFormFile file)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(imagepath, covername);
            using var stream = File.Create(path);
            await file.CopyToAsync(stream);
            return covername;
        }

    }
}
