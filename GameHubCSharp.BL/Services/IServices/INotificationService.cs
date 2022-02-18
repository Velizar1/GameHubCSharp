using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface INotificationService : IBaseService
    {
        public Task<Notification> AddAsync(Notification notification);
        public Task<Notification> DeleteAsync(Notification notification);
        public List<Notification> GetForEvent(GameEvent gameEvent);

       

    }
}
