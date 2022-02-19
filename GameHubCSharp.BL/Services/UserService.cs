using GameHubCSharp.BL.Services.IServices;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(ApplicationDbContext dbContext, IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Notification> AddNotificationAsync(Notification notification, string userId)
        {
            //var user = db.Users.Where(u => u.Id.ToString() == userId).First();
            //user.NotificationsRecived.Add(notification);
            //db.SaveChanges();
            //return notification;

            var user = repository
                .All<User>(u => u.Id.ToString() == userId)
                .FirstOrDefault();

            user.NotificationsRecived.Add(notification);

            await repository.SaveChangesAsync();

            return notification;
        }

        public async Task<List<Notification>> ChangeStatusAsync(string userName)
        {
            //var user = db.Users.FirstOrDefault(u => u.UserName == userName);
            var user = repository
                .All<User>(x => x.UserName == userName)
                .FirstOrDefault();

            var notifications = repository
                .All<Notification>(n => n.SenderId == user.Id);

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await repository.SaveChangesAsync();
            return notifications.ToList();
        }

        public async Task DeleteAsync(string id)
        {
            //var user = db.Users.FirstOrDefault(x => x.Id.ToString() == id);
            var user = repository
                .All<User>(x => x.Id.ToString() == id)
                .FirstOrDefault();

            await repository.DeleteAsync(user);
            await repository.SaveChangesAsync();
        }

        public List<User> FindAll()
        {
            return repository
                .All<User>()
                .ToList();
        }

        public List<Notification> FindAllNotifications(string userName)
        {
            //return db.Users.FirstOrDefault(u => u.UserName == userName).Notifications.OrderBy(x => x.CreatedAt).ToList();
            return repository.All<User>()
                .FirstOrDefault(x => x.UserName == userName)
                .NotificationsRecived
                .OrderBy(x => x.CreatedAt)
                .ToList();
        }


        public User FindUserById(string userId)
        {
            var user = repository
                .All<User>(u => u.Id.ToString() == userId)
                .FirstOrDefault();

            return user;
        }

        public User FindUserByName(string userName)
        {
            var user = repository
                .All<User>(u => u.UserName == userName)
                .FirstOrDefault();

            return user;
        }
    }
}