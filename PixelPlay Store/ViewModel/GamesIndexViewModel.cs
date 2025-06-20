namespace PixelPlay.ViewModel
{
    public class GamesIndexViewModel
    {
        public List<Games>? PagedGames { get; set; }
        public List<Games>? ActionGames { get; set; }
        public List<Games>? AdventureGames { get; set; }
        public List<Games>? ArcadeGames { get; set; }
        public List<Games>? HorrorGames { get; set; }
        public List<Games>? FightingGames { get; set; }
        public List<Games>? StoryGames { get; set; }
        public List<Games>? ShootingGames { get; set; }
        public List<Games>? SportGames { get; set; }
        public List<Games>? SurvivalGames { get; set; }
        public List<Games>? DramaGames { get; set; }
        public List<Categories>? Categories { get; set; }
    }
}
