using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.DAL.Data.Models
{
    public class User : IdentityUser<Guid>
    {

        public bool IsDeleted { get; set; }

        public int Rating { get; set; }

        public virtual ICollection<Notification> NotificationsSend { get; set; }

        public virtual ICollection<Notification> NotificationsRecived { get; set; }

        public User()
        {
            NotificationsSend = new List<Notification>();
            NotificationsRecived = new List<Notification>();
        }
    }
}
