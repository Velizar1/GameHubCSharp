using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Data.Models
{
    public class Player : BaseModel
    {

        private User user;
        private String usernameInGame;
        private ICollection<GameEvent> gameEvents;

        [Required]
        public virtual User User { get => user; set => user = value; }
        [Required]
        [MinLength(1),MaxLength(20)]
        public string UsernameInGame { get => usernameInGame; set => usernameInGame = value; }
        
        public virtual ICollection<GameEvent> GameEvents { get => gameEvents; set => gameEvents = value; }

        public Player()
        {
            GameEvents = new List<GameEvent>();
        }


    }
}
