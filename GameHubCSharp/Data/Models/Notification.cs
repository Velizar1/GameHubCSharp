using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Data.Models
{
    public class Notification : BaseModel
    {

        private string message;
        private GameEvent gameEvent;
        private User from;
        private User to;

        [Required]
        public string Message { get => message; set => message = value; }
        public virtual GameEvent GameEvent { get => gameEvent; set => gameEvent = value; }
        [Required]
        public virtual User From{ get => from; set => from = value; }
        [Required]
        public virtual User To { get => to; set => to = value; }
    }
}
