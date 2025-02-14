
namespace PixelPlay.Repositories.Repos
{
    public class GamesRepo : IGameRepo
    {
        private readonly MyDbContext context;
        public GamesRepo(MyDbContext myDbContext)
        {
            context = myDbContext;
        }

        public void Create(Games game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game cannot be null.");
            }
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
