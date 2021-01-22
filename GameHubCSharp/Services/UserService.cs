﻿using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using System.Linq;

namespace GameHubCSharp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User FindUserById(string userId)
        {
            var user = dbContext.Users.Where(x => x.Id.ToString() == userId).First();

            return user;
        }

        public User FindUserByName(string userName)
        {
            var user = dbContext.Users.Where(x => x.UserName == userName).First();

            return user;
        }
    }
}