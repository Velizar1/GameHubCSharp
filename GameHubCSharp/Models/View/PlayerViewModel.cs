using System;
using System.Collections.Generic;

namespace GameHubCSharp.Models.View
{
    public class PlayerViewModel
    {
        private string id;
        private string username;
        private String usernameInGame;
        private ICollection<GameEventViewModel> gameEvents;

        public string UsernameInGame { get => usernameInGame; set => usernameInGame = value; }
        public ICollection<GameEventViewModel> GameEvents { get => gameEvents; set => gameEvents = value; }
        public string Username { get => username; set => username = value; }
        public string Id { get => id; set => id = value; }
    }
}