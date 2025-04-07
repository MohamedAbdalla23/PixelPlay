﻿
using PixelPlay.Repositories.ReposInterface;
using System.Threading.Tasks;

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

        public bool Delete(int id)
        {
            var isdeleted = false;
            var game = context.Games.Find(id);
            if (game == null)            
                return isdeleted;
            
            context.Remove(game);
            var effectedrows = context.SaveChanges();
            if (effectedrows > 0)
            {
                isdeleted = true;
                var cover = Path.Combine(imagepath, game.Cover);
                File.Delete(cover);
            }
            return isdeleted;
        }

        public IQueryable<Games> GetAll()
        {
            return context.Games
                .Include(g => g.GameDevices)
                .ThenInclude(d => d.Device)
                .Include(g => g.GameCategories)
                .ThenInclude(d => d.Category)
                .AsNoTracking() ??
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

        public async Task<Games?> UpdateGameAsync(EditGameFormViewModel model)
        {
            var game = context.Games
                .Include(d => d.GameDevices)
                .Include(c => c.GameCategories)
                .FirstOrDefault(x => x.Id == model.Id);

            if (game is null)
                return null;

            var oldcover = game.Cover;
            var hasNewCover = model.Cover is not null;

            game.Name = model.Name;
            game.Description = model.Description;

            context.GameDevices.RemoveRange(game.GameDevices);
            context.GameCategories.RemoveRange(game.GameCategories);

            game.GameDevices = model.GameDevices.Select(d => new GameDevices { DeviceId = d }).ToList();
            game.GameCategories = model.GameCategories.Select(c => new GameCategories { CategoryId = c }).ToList();

            string? newCoverPath = null;
            if (hasNewCover)
            {
                newCoverPath = await gameformrepo.SaveCover(model.Cover!);
                game.Cover = newCoverPath;
            }

            var affectedRows = await context.SaveChangesAsync();
            if (affectedRows > 0)
            {
                if (hasNewCover && File.Exists(Path.Combine(imagepath, oldcover)))
                {
                    try { File.Delete(Path.Combine(imagepath, oldcover)); } catch { /* optionally log */ }
                }
                return game;
            }
            else
            {
                // Rollback: delete the newly saved file if SaveChanges failed
                if (hasNewCover && newCoverPath is not null && File.Exists(Path.Combine(imagepath, newCoverPath)))
                {
                    try { File.Delete(Path.Combine(imagepath, newCoverPath)); } catch { /* optionally log */ }
                }
                return null;
            }
        }

    }
}
