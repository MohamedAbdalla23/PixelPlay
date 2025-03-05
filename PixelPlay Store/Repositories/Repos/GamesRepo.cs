
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
            imagepath = $"{webhostenvironment.WebRootPath}/assets/images";
        }

        public void Create(GameFormViewModel game)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(game.Cover.FileName)}";
            var path = Path.Combine(imagepath, covername);
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

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Games games)
        {
            context.Update(games);
        }
    }
}
