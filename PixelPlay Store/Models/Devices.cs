namespace PixelPlay.Models
{
    public class Devices : BaseEntity
    {
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;


        public ICollection<GameDevices> GameDevices { get; set; } = new List<GameDevices>();
    }
}
