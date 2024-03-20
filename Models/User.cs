using System.ComponentModel.DataAnnotations;

namespace AuthCrudApp.Models
{
    public class User
    {

        public int Userid { get; set; }
        [Required]
        
        [RegularExpression("^[a-zA-Z0-9.\\-_$@*!]{3,30}$", ErrorMessage = "Please enter a valid User Name.")]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30)]
        //[EmailAddress]
        [RegularExpression("^[\\w\\.-]+@[\\w\\.-]+\\.\\w+$", ErrorMessage = "Please enter a valid User Email.")]
        public string Useremail { get; set; }
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

    }
}
