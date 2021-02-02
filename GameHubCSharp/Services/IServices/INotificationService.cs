using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services.IServices
{
    public interface INotificationService
    {
        public Notification Add(Notification notification);
        public Notification Delete(Notification notification);
        public List<Notification> GetForEvent(GameEvent gameEvent);

       

    }
}
