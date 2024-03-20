using System.ComponentModel.DataAnnotations;

namespace AuthCrudApp.ViewModels
{
    public class LoginModel
    {
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
