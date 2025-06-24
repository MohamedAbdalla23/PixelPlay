namespace PixelPlay.ViewModel
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }

        public int TotalRoles { get; set; }

        public List<ApplicationUser> RecentUsers { get; set; } = new();
    }
}
