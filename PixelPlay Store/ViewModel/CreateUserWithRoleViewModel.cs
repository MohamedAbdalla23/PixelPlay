namespace PixelPlay.ViewModel
{
    public class CreateUserWithRoleViewModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password"), DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Assign Role")]
        public string SelectedRole { get; set; } = string.Empty;

        public List<SelectListItem> Roles { get; set; } = new();
    }
}
