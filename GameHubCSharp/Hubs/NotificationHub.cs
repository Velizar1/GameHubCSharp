using AutoMapper;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.Models;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.BL.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.BL.Services.IServices;

namespace GameHubCSharp.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ApplicationDbContext db;
        private readonly IUserService userService;
        private readonly IGameEventService gameEventService;
        private readonly IMapper mapper;
        private readonly IPlayerService playerService;

        public NotificationHub(ApplicationDbContext db, IUserService userService, IGameEventService gameEventService, IMapper mapper, IPlayerService playerService)
        {
            this.db = db;
            this.userService = userService;
            this.gameEventService = gameEventService;
            this.mapper = mapper;
            this.playerService = playerService;
        }
        public async Task SendNotificationTo(string roomId)
        {
            var ownerId = db.GameEvents.FirstOrDefault(x => x.Id.ToString() == roomId).OwnerId;
            var player = db.Players.FirstOrDefault(x => x.Id.ToString() == ownerId);
            var list = player.User.Notifications;
            await this.Clients.User(ConnectionIdProvider.ids[player.User.UserName]).SendAsync("ReceiveNotfication", new
            {
                Notifications = list.OrderByDescending(x=>x.CreatedAt).ToArray(),
                NotCount = player.User.Notifications.Count(n => n.IsRead == false)
            }) ;
        }
        public async Task SendNotificationToUser(string userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id.ToString() == userId); 
            var list =user.Notifications;
            await this.Clients.User(ConnectionIdProvider.ids[user.UserName]).SendAsync("ReceiveNotfication", new
            {
                Notifications = list.OrderByDescending(x=>x.CreatedAt).ToArray(),
                NotCount = user.Notifications.Count(n => n.IsRead == false)
            });
        }
        public async Task NotificationCount(string user)
        {
            var nots = userService.ChangeStatus(user);
            await this.Clients.User(ConnectionIdProvider.ids[user]).SendAsync("UpdateNotifications", new { Notifications = nots.OrderByDescending(x=>x.CreatedAt).ToArray() });
        }
        public override Task OnConnectedAsync()
        {
            try
            {
                ConnectionIdProvider.ids[Context.User.Identity.Name] = Context.UserIdentifier;//ConectionId of Guests and Users
            }
            catch { }
            
            return base.OnConnectedAsync();
        }

        /////////////
        public async Task UpdateEvents()
        {
            var list = gameEventService.FindAll().ToList();
            var list2 = list.Select(x => {
                var re = mapper.Map<HomeEventRestViewModel>(x);
                re.OwnerName = playerService.FindPlayerById(x.OwnerId.ToString()).UsernameInGame;
                re.ImageUrl = x.Game.ImageUrl;
                re.TakenPlaces = x.NumberOfPlayers;
                return re;
            })
                .ToList();
            await this.Clients.All.SendAsync("UpdateEventList", new
            {
                GameEvents = list2.ToArray().OrderBy(x=>x.StartDate)
            });
        }
    }
}
