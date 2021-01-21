using System.ComponentModel.DataAnnotations;

namespace GameHubCSharp.Controllers
{
    public class RegistartionViewModel
    {
        [Required]
        [MinLength(1),MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [MinLength(1), MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(1), MaxLength(20)]
        [Compare(nameof(Password),ErrorMessage ="The passwords must be indentical!")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get;  set; }
    }
}
