using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.DAL.Repositories.Interfaces;

namespace GameHubCSharp.BL.Services
{
    public class NotificationService : BaseService, INotificationService
    {

        public NotificationService(IRepository _repository) : base(_repository)
        {

        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            await repository.CreateAsync(notification);
            return notification;
        }

        public async Task<Notification> DeleteAsync(Notification notification)
        {
            var notif = repository.All<Notification>()
                .FirstOrDefault(n => n.Id == notification.Id);

            if (notif != null)
            {
                await repository.DeleteAsync(notif); 
            }
            return notif;
        }

        public List<Notification> GetForEvent(GameEvent gameEvent)
        {
            return repository.AllReadOnly<Notification>()
                .Where(n => n.GameEvent.Id == gameEvent.Id)
                .ToList();
        }
    }
}
