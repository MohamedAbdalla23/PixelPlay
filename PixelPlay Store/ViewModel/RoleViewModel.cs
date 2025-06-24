namespace PixelPlay.ViewModel
{
    [Keyless]
    public class RoleViewModel
    {
        [Required, Display(Name = "Role Name")]
        public string? RoleName { get; set; }


    }
}
