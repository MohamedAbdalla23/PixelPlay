namespace PixelPlay.ViewModel
{
    public class DeviceGameCountViewModel
    {
        public int Id { get; set; }

        public string? Icon { get; set; }

        public string? Name { get; set; }

        [Display(Name = "No. of games associated with device")]
        public int NoGameCount { get; set; }
    }
}
