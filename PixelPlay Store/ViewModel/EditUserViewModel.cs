namespace PixelPlay.ViewModel
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        // Role-related
        public string? SelectedRole { get; set; }
        public List<SelectListItem> Roles { get; set; } = new();
    }
}
