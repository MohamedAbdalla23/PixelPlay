
namespace PixelPlay.Repositories.Repos
{
    public class GamesRepo : IGameRepo
    {
        private readonly MyDbContext context;
        private readonly IWebHostEnvironment webhostenvironment;
        private readonly string imagepath;
        public GamesRepo(MyDbContext myDbContext, IWebHostEnvironment webHostenvironment)
        {
            context = myDbContext;
            webhostenvironment = webHostenvironment;
            imagepath = $"{webhostenvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task Create(GameFormViewModel model)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(imagepath, covername);
            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);

            Games game = new()
            {
                Name = model.Name,
                Description = model.Description,
                Cover = covername,
                GameCategories = model.GameCategories.Select(c => new GameCategories { CategoryId = c }).ToList(),
                GameDevices = model.GameDevices.Select(d => new GameDevices { DeviceId = d }).ToList()
            };
            context.Games.Add(game);            
        }

        public void Delete(int id)
        {
            var game = context.Games.Find(id);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {id} was not found.");
            }
            context.Remove(game);             
        }

        public List<Games> GetAll()
        {
            return context.Games.ToList() ??
                throw new InvalidOperationException("There was no Games to be found!");
        }

        public Games GetById(int id)
        {
            return context.Games.FirstOrDefault(x => x.Id == id);                 
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Games games)
        {
            context.Update(games);
        }
    }
}
