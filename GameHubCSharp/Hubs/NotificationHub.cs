using GameHubCSharp.Data;
using GameHubCSharp.Models;
using GameHubCSharp.Services;
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
        private readonly IUserService userService;

        public NotificationHub(ApplicationDbContext db, IUserService userService)
        {
            this.db = db;
            this.userService = userService;
        }
        public async Task SendNotificationTo(string id)
        {
            var owner = db.GameEvents.FirstOrDefault(x => x.Id.ToString() == id).OwnerId;
            var player = db.Players.FirstOrDefault(x => x.Id.ToString() == owner);
                await this.Clients.User(ConnectionIdProvider.ids[player.User.UserName]).SendAsync("ReceiveNotfication",new { Notifications = player.User.Notifications.ToArray() ,
                    NotCount= player.User.Notifications.Count(n=>n.IsRead==false)});
        }

        public async Task NotificationCount(string user)
        {
            var nots = userService.ChangeStatus(user);
            await this.Clients.User(ConnectionIdProvider.ids[user]).SendAsync("UpdateNotifications", new { Notifications = nots.ToArray() });
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
