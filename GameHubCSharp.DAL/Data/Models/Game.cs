using System.ComponentModel.DataAnnotations;

namespace GameHubCSharp.DAL.Data.Models
{
    public class Game : BaseModel
    {

        [Required]
        [MinLength(1),MaxLength(20)]
        public string GameName { get; set; }
       
        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}