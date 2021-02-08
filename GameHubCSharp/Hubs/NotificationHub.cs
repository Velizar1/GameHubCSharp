using AutoMapper;
using GameHubCSharp.Data;
using GameHubCSharp.Models;
using GameHubCSharp.Models.View;
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
        public async Task SendNotificationTo(string id)
        {
            var owner = db.GameEvents.FirstOrDefault(x => x.Id.ToString() == id).OwnerId;
            var player = db.Players.FirstOrDefault(x => x.Id.ToString() == owner);
            var list = player.User.Notifications;
            await this.Clients.User(ConnectionIdProvider.ids[player.User.UserName]).SendAsync("ReceiveNotfication", new
            {
                Notifications = list.ToArray(),
                NotCount = player.User.Notifications.Count(n => n.IsRead == false)
            }) ;
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
                GameEvents = list2.ToArray()
            });
        }
    }
}
