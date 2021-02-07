using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameHubCSharp.Data.Models
{
    public class Notification : BaseModel
    {

        private string message;
        private GameEvent gameEvent;
        private Guid gameEventId;
        private string fromUser;
        private bool isRead;
        private string toUser;

        [Required]
        public string Message { get => message; set => message = value; }
        [JsonIgnore]
        public virtual GameEvent GameEvent { get => gameEvent; set => gameEvent = value; }
        [Required]
        public  string From{ get => fromUser; set => fromUser = value; }
        [Required]
        public  string To { get => toUser; set => toUser = value; }
        public bool IsRead { get => isRead; set => isRead = value; }
        public Guid GameEventId { get => gameEventId; set => gameEventId = value; }
    }
}
