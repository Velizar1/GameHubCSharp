using System;
using System.Collections.Generic;

namespace GameHubCSharp.BL.Models.DTO
{
    public class PlayerViewModel
    {
        private Guid id;
        private string username;
        private String usernameInGame;
        private ICollection<GameEventViewModel> gameEvents;
        private bool status;

        public string UsernameInGame { get => usernameInGame; set => usernameInGame = value; }
        public ICollection<GameEventViewModel> GameEvents { get => gameEvents; set => gameEvents = value; }
        public string Username { get => username; set => username = value; }
        public Guid Id { get => id; set => id = value; }
        public bool Status { get => status; set => status = value; }
    }
}