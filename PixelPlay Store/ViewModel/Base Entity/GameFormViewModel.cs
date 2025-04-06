namespace PixelPlay.ViewModel.Base_Entity
{
    public class GameFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;      

        [Display(Name = "Game Devices")]
        public List<int> GameDevices { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Game Categories")]
        public List<int> GameCategories { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}

