using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext dbContext;

        public NotificationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Notification Add(Notification notification)
        {
            dbContext.Notifications.Add(notification);
            dbContext.SaveChanges();
            return notification;
        }

        public Notification Delete(Notification notification)
        {
            var notiff = dbContext.Notifications.Where(n => n.Id == notification.Id).FirstOrDefault();
            if (notiff != null)
            {
                dbContext.Remove(notiff);
                dbContext.SaveChanges();
                return notiff;
            }
            return null;
        }

        public List<Notification> GetForEvent(GameEvent gameEvent)
        {
            return dbContext.Notifications.Where(n => n.GameEvent.Id == gameEvent.Id).ToList();
        }
    }
}
