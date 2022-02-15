using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.DAL.Data.Models
{
    public class Player : BaseModel
    {

        public Guid UserId { get; set; }

        public bool Status { get; set; }

        [Required]
        [MinLength(1), MaxLength(20)]
        public string UsernameInGame { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual ICollection<GameEvent> GameEventsOwn { get; set; }

        public virtual ICollection<GameEvent> GameEventsParticipates { get; set; }

        public Player()
        {
            GameEventsOwn = new List<GameEvent>();
            GameEventsParticipates = new List<GameEvent>();
        }

    }
}
