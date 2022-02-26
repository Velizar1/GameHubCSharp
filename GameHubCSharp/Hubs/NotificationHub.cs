using AutoMapper;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.BL.Services.IServices;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
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


        //----Notifications------------------
        public NotificationHub(ApplicationDbContext db,
            IUserService userService,
            IGameEventService gameEventService,
            IMapper mapper,
            IPlayerService playerService)
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
            var player = db.Players.FirstOrDefault(x => x.Id == ownerId);
            var list = player.User.NotificationsRecived;
            await this.Clients.User(ConnectionIdProvider.ids[player.User.UserName]).SendAsync("ReceiveNotfication", new
            {
                Notifications = list.OrderByDescending(x => x.CreatedAt).ToArray(),
                NotCount = player.User.NotificationsRecived.Count(n => n.IsRead == false)
            });
        }
        public async Task SendNotificationToUser(string userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id.ToString() == userId);
            var list = user.NotificationsRecived;
            await this.Clients.User(ConnectionIdProvider.ids[user.UserName]).SendAsync("ReceiveNotfication", new
            {
                Notifications = list.OrderByDescending(x => x.CreatedAt).ToArray(),
                NotCount = user.NotificationsRecived.Count(n => n.IsRead == false)
            });
        }
        public async Task NotificationCount(Guid userId)
        {
            var nots = await userService.ChangeNotificationStatusToReadAsync(userId); // Change to user name
            await this.Clients.User(ConnectionIdProvider.ids[userId.ToString()]).SendAsync("UpdateNotifications", new { Notifications = nots.OrderByDescending(x => x.CreatedAt).ToArray() });
        }
        public override Task OnConnectedAsync()
        {
                ConnectionIdProvider.ids[Context.User.Identity.Name] = Context.UserIdentifier;//ConectionId of Guests and Users

            return base.OnConnectedAsync();
        }

        //public override Task OnDisconnectedAsync(Exception exception)
        //{
        //    ConnectionIdProvider.ids.Clear();
        //    ConnectionIdProvider.notifications.Clear();
        //    ConnectionIdProvider.events.Clear();
        //    return base.OnDisconnectedAsync(exception);
        //}

        //---------GameEvents---------------
        public async Task UpdateEvents()
        {
            var list = gameEventService.FindAll().ToList();
            var list2 = list.Select(x =>
            {
                var re = mapper.Map<HomeEventRestViewModel>(x);
                re.OwnerName = playerService.FindById(x.OwnerId).UsernameInGame;
                re.ImageUrl = x.Game.ImageUrl;
                re.TakenPlaces = x.NumberOfPlayers;
                return re;
            })
                .ToList();
            await this.Clients.All.SendAsync("UpdateEventList", new
            {
                GameEvents = list2.ToArray().OrderBy(x => x.StartDate)
            });
        }
    }
}
