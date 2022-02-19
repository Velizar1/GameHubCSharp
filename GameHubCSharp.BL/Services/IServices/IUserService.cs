using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IUserService : IBaseService
    {
        public User FindUserById(Guid userId);
        public User FindUserByName(string userName);
        public List<User> FindAll();
        public Task DeleteAsync(Guid id);
        public Task<List<Notification>> ChangeNotificationStatusToReadAsync(Guid userId);
        public List<Notification> FindAllNotificationsByUserId(Guid userId);
        public List<Notification> FindAllNotificationsByUserName(string name);
        public Task<Notification> AddNotificationAsync(Notification notification ,Guid userId);
    }
}