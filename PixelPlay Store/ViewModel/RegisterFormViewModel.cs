namespace PixelPlay.ViewModel
{
    [Keyless]
    public class RegisterFormViewModel
    {      
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress] 
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfrmPass { get; set; }

        [Required]
        [Phone] 
        public string? Phone { get; set; }

        [Required]
        [Display(Name = "Home Address")]
        public string? Address { get; set; }
    }
}
