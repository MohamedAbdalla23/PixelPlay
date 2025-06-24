namespace PixelPlay.ViewModel
{
    [Keyless]
    public class LoginFormViewModel
    {
        [Required, Display(Name = "Username/Email")]
        public string? LoginInput { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me!")]
        public bool RememberMe { get; set; }
    }
}
