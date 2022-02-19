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
    public class NotificationService : INotificationService
    {
        private readonly IRepository repository;

        public NotificationService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            await repository.CreateAsync(notification);
            await repository.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> DeleteAsync(Notification notification)
        {
            var notiff = repository
                .All<Notification>()
                .FirstOrDefault(n => n.Id == notification.Id);

            if (notiff != null)
            {
                await repository.DeleteAsync(notiff);
                await repository.SaveChangesAsync();
                return notiff;
            }
            return null;
        }

        public List<Notification> GetForEvent(GameEvent gameEvent)
        {
            return repository
                .All<Notification>()
                .Where(n => n.GameEvent.Id == gameEvent.Id)
                .ToList();
        }
    }
}
