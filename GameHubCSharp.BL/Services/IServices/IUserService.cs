using GameHubCSharp.DAL.Data.Models;
using System.Collections.Generic;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IUserService
    {
        public User FindUserById(string userId);
        public User FindUserByName(string userId);
        public List<User> FindAll();
        public void Delete(string id);

        public List<Notification> ChangeStatus(string userName);
        public List<Notification> FindAllNotifications(string userName);
        public Notification AddNotification(Notification notification ,string userId);
    }
}