namespace PixelPlay.Models
{
    public class GameDevices
    {
        public int GameId { get; set; }
        
        public Games Game { get; set; }

                

        public int DeviceId { get; set; }

        public Devices Device { get; set; }
    }
}
