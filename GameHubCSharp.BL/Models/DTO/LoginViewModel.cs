using System.ComponentModel.DataAnnotations;

namespace GameHubCSharp.Controllers
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(1), MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(1), MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

    }
}
