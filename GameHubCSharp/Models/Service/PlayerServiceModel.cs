using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Models.Service
{
    public class PlayerServiceModel : BaseServiceModel
    {
        private UserServiceModel user;
        private String usernameInGame;
        private ICollection<GameEventServiceModel> gameEvents;

        public string UsernameInGame { get => usernameInGame; set => usernameInGame = value; }
        public UserServiceModel User { get => user; set => user = value; }
        public ICollection<GameEventServiceModel> GameEvents { get => gameEvents; set => gameEvents = value; }
    }
}
