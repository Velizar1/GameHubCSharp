using System.ComponentModel.DataAnnotations;

namespace GameHubCSharp.Controllers
{
    public class RegistartionViewModel
    {
        [Required]
        [MinLength(1), MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password min length is 6 characters"), MaxLength(20, ErrorMessage = "Password min length is 20 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password min length is 6 characters"), MaxLength(20, ErrorMessage = "Password min length is 20 characters")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
