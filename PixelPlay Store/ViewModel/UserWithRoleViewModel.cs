namespace PixelPlay.ViewModel
{
    public class UserWithRoleViewModel
    {
        public int Id { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
