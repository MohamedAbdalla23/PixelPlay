namespace PixelPlay.ViewModel
{
    public class CategoryGameCountViewModel
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        [Display(Name = "No. of games associated with category")]
        public int NoGameCount { get; set; }
        
    }
}
