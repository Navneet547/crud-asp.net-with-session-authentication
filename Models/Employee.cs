using System.ComponentModel.DataAnnotations;

namespace AuthCrudApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please enter a valid Name.")]
        [MaxLength(20)]
        public string? EmployeeName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Please enter a valid Gender Name.")]
        [MaxLength(10)]
        public string? EmployeeGender { get; set; }
        [Required]
        //[MaxLength(2)]
        public int EmployeeAge { get; set; }
        [Required]
        [MaxLength(100)]
        public string? EmployeeAddress { get; set; }
    }
}
