using GameHubCSharp.Data;
using GameHubCSharp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ApplicationDbContext db;

        public NotificationHub(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task SendNotificationTo(string roomid)
        {
            var owner = db.GameEvents.FirstOrDefault(x => x.Id.ToString() == roomid).OwnerId;
            var user = db.Users.FirstOrDefault(x => x.Id.ToString() == owner);
                await this.Clients.User(ConnectionIdProvider.ids[user.UserName]).SendAsync("ReceiveNotfication");
        }

        public override Task OnConnectedAsync()
        {
            try
            {
                ConnectionIdProvider.ids[Context.User.Identity.Name] = Context.UserIdentifier;
            }
            catch { }
            
            return base.OnConnectedAsync();
        }
    }
}
