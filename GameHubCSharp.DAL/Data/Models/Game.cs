using System.ComponentModel.DataAnnotations;

namespace GameHubCSharp.DAL.Data.Models
{
    public class Game : BaseModel
    {

        private string gameName;
        private string imageUrl;

        [Required]
        [MinLength(1),MaxLength(20)]
        public string GameName { get => gameName; set => gameName = value; }
        [Required]
        [Url]
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}