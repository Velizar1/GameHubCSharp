using GameHubCSharp.Data;
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
        public async Task SendNotificationTo(string user)
        {
            var a = Context.UserIdentifier;
            await this.Clients.All.SendAsync("ReceiveNotfication",user);
        
        }
    }
}
