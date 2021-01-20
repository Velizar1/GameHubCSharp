namespace GameHubCSharp.Data.Models
{
    public class Game : BaseModel
    {

        private GameName gameName;
        private string imageUrl;

        public GameName GameName { get => gameName; set => gameName = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}