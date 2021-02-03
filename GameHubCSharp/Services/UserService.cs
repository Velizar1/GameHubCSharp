﻿using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameHubCSharp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public Notification AddNotification(Notification notification,string userId)
        {
            var user = db.Users.Where(u => u.Id.ToString() == userId).First();
            user.Notifications.Add(notification);
            db.SaveChanges();
            return notification;
        }

        public void Delete(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id.ToString() == id);
            db.Remove(user);
            db.SaveChanges();
        }

        public List<User> FindAll()
        {
            return db.Users.ToList();
        }

        public List<Notification> FindAllNotifications(string userName)
        {
            return db.Users.FirstOrDefault(u=>u.UserName==userName).Notifications;
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