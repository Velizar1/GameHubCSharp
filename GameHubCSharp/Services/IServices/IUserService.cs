using GameHubCSharp.Data.Models;
using System.Collections.Generic;

namespace GameHubCSharp.Services
{
    public interface IUserService
    {
        public User FindUserById(string userId);
        public User FindUserByName(string userId);
        public List<User> FindAll();
        public void Delete(string id);
    }
}