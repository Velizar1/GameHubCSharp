using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Data.Models
{
    public class Player : BaseModel
    {

        private User user;
        private String usernameInGame;
        private HashSet<GameEvent> gameEvents;

        public User User { get => user; set => user = value; }
        public string UsernameInGame { get => usernameInGame; set => usernameInGame = value; }
        public HashSet<GameEvent> GameEvents { get => gameEvents; set => gameEvents = value; }

        public int GameEventId { get; set; }
    }
}
