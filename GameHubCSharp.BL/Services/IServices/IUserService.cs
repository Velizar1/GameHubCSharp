using GameHubCSharp.DAL.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IUserService : IBaseService
    {
        public User FindUserById(string userId);
        public User FindUserByName(string userId);
        public List<User> FindAll();
        public Task DeleteAsync(string id);

        public Task<List<Notification>> ChangeStatusAsync(string userName);
        public List<Notification> FindAllNotifications(string userName);
        public Task<Notification> AddNotificationAsync(Notification notification ,string userId);
    }
}