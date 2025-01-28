namespace PixelPlay.Models
{
    public class Categories : BaseEntity
    {
        public ICollection<GameCategories> GameCategories { get; set; } = new List<GameCategories>();
    }
}
