
namespace PixelPlay.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? Address { get; set; }
    }
}
