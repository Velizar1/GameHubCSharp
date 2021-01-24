namespace GameHubCSharp.Models.View
{
    public class GameViewModel
    {
        private string gameName;
        private string imageUrl;

        public string GameName { get => gameName; set => gameName = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}