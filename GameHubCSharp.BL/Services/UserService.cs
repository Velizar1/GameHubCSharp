using GameHubCSharp.BL.Services.IServices;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
{
    public class UserService : BaseService, IUserService
    {

        public UserService(IRepository _repository) : base(_repository)
        {

        }

        public async Task<Notification> AddNotificationAsync(Notification notification, Guid userId)
        {
            var user = await repository
                .All<User>(u => u.Id == userId)
                .Include(x => x.NotificationsRecived)
                .FirstOrDefaultAsync();

            if (!user.NotificationsRecived.Contains(notification))
            {
                user.NotificationsRecived.Add(notification);
            }

            return notification;
        }

        public async Task<List<Notification>> ChangeNotificationStatusToReadAsync(Guid userId)
        {
            var notifications = repository.All<Notification>(n => n.RecipientId == userId);

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            return notifications.ToList();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = repository
                .All<User>(x => x.Id == id)
                .FirstOrDefault();

            await repository.DeleteAsync(user);
        }

        public List<User> FindAll()
        {
            return repository.AllReadOnly<User>()
                .ToList();
        }

        public List<Notification> FindAllNotificationsByUserId(Guid id)
        {
            return repository.AllReadOnly<User>()
                .Include(x => x.NotificationsRecived)
                .Where(x => x.Id == id)
                .Select(x => x.NotificationsRecived
                             .OrderBy(x => x.CreatedAt))
                .FirstOrDefault()
                .ToList();
        }

        public List<Notification> FindAllNotificationsByUserName(string name)
        {
            return repository.AllReadOnly<User>()
                .Include(x => x.NotificationsRecived)
                .Where(x => x.UserName == name)
                .Select(x => x.NotificationsRecived
                             .OrderBy(x => x.CreatedAt))
                .FirstOrDefault()
                .ToList();
        }

        public User FindUserById(Guid userId)
        {
            return repository.AllReadOnly<User>(u => u.Id == userId)
                .FirstOrDefault();
        }

        public User FindUserByName(string userName)
        {
            return repository.AllReadOnly<User>(u => u.UserName == userName)
                .FirstOrDefault();
        }

    }
}