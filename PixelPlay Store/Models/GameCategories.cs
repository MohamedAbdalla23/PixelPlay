namespace PixelPlay.Models
{
    public class GameCategories
    {
        public int GameId { get; set; }

        public Games Game { get; set; }


        public int CategoryId { get; set; }

        public Categories Category { get; set; }
    }
}
