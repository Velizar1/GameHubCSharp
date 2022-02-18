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
        private readonly ApplicationDbContext db;
        private readonly IRepository repository;

        public UserService(ApplicationDbContext dbContext, IRepository repository)
        {
            this.db = dbContext;
            this.repository = repository;
        }

        public Notification AddNotification(Notification notification,string userId)
        {
            var user = db.Users.Where(u => u.Id.ToString() == userId).First();
            user.NotificationsRecived.Add(notification);
            db.SaveChanges();
            return notification;
        }

        public async Task<List<Notification>> ChangeStatus(string userName)
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

            await repository.SavechangesAsync();
            return notifications.ToList();
        }

        public void Delete(string id)
        {
            //var user = db.Users.FirstOrDefault(x => x.Id.ToString() == id);
            var user = repository.All<User>(x=>x.Id.ToString() == id).FirstOrDefault();
            db.Remove(user);
            db.SaveChanges();
        }

        public List<User> FindAll()
        {
            //return db.Users.ToList();
            return repository.All<User>().ToList();
        }

        public List<Notification> FindAllNotifications(string userName)
        {
            //return db.Users.FirstOrDefault(u => u.UserName == userName).Notifications.OrderBy(x => x.CreatedAt).ToList();
            return repository.All<User>()
                .FirstOrDefault(x => x.UserName == userName)
                .Notifications
                .OrderBy(x => x.CreatedAt)
                .ToList();
        }


        public User FindUserById(string userId)
        {
            var user = db.Users.Where(x => x.Id.ToString() == userId).First();

            return user;
        }

        public User FindUserByName(string userName)
        {
            var user = db.Users.Where(x => x.UserName == userName).First();

            return user;
        }
    }
}