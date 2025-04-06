
using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Repositories.Repos
{
    public class GamesRepo : IGameRepo
    {
        private readonly MyDbContext context;
        private readonly IGameForm gameformrepo;
        private readonly IWebHostEnvironment webhostenvironment;
        private readonly string imagepath;
        public GamesRepo(MyDbContext myDbContext, IGameForm gameForm, IWebHostEnvironment webHostenvironment)
        {
            context = myDbContext;
            gameformrepo = gameForm;
            webhostenvironment = webHostenvironment;
            imagepath = $"{webhostenvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

        public async Task Create(CreateGameFormViewModel model)
        {
            var covername = await gameformrepo.SaveCover(model.Cover);

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

        public IQueryable<Games> GetAll()
        {
            return context.Games
                .Include(g => g.GameDevices)
                .ThenInclude(d => d.Device)
                .Include(g => g.GameCategories)
                .ThenInclude(d => d.Category)
                .AsNoTracking()                 ??
                throw new InvalidOperationException("There was no Games to be found!");
        }

        public Games GetById(int id)
        {
            return context.Games
                .Include(g => g.GameDevices)
                .ThenInclude(d => d.Device)
                .Include(g => g.GameCategories)
                .ThenInclude(d => d.Category)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id)!;                 
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<Games?> Update(EditGameFormViewModel model)
        {
            var game = context.Games
                .Include(d => d.GameDevices)
                .Include(c => c.GameCategories)
                .FirstOrDefault(x => x.Id == model.Id);
            var hasnewcover = model.Cover is not null;
            var oldcover = game.Cover;
            if (game is null)
            {
                return null;
            }
            game.Name = model.Name;
            game.Description = model.Description;
            game.GameDevices = model.GameDevices.Select(d => new GameDevices { DeviceId = d }).ToList();
            game.GameCategories = model.GameCategories.Select(c => new GameCategories { CategoryId = c }).ToList();
            if (hasnewcover)
            {
                game.Cover = await gameformrepo.SaveCover(model.Cover!);
            }
            var effectedrows = context.SaveChanges();
            if (effectedrows > 0)
            {
                if (hasnewcover)
                {
                    var cover = Path.Combine(imagepath,oldcover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(imagepath, game.Cover);
                File.Delete(cover);

                return null;
            }           
        }
    }
}
