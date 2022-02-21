using AutoMapper;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.Hubs;
using GameHubCSharp.Models;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.BL.Services;
using GameHubCSharp.BL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Controllers
{
    public class GameEventController : Controller
    {
        private ApplicationDbContext db;
        private IGameEventService gameEventService;
        private readonly IMapper mapper;
        private readonly IGameService gameService;
        private readonly IPlayerService playerService;
        private readonly IUserService userService;
        private readonly IHubContext<NotificationHub> hub;
        private readonly INotificationService notificationService;

        public GameEventController(ApplicationDbContext db,
            IGameEventService gameEventService,
            IMapper mapper,
            IGameService gameService,
            IPlayerService playerService,
            IUserService userService,
            IHubContext<NotificationHub> hub,
            INotificationService notificationService)
        {
            this.db = db;
            this.gameEventService = gameEventService;
            this.mapper = mapper;
            this.gameService = gameService;
            this.playerService = playerService;
            this.userService = userService;
            this.hub = hub;
            this.notificationService = notificationService;
        }

        [HttpGet("/game/detail/")]
        public IActionResult GameEventDetail(Guid id, bool? valid = true)
        {
            var gameEvent = gameEventService.FindEventById(id);
            if (gameEvent == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var gameEve = mapper.Map<GameEventViewModel>(gameEvent);
            gameEve.OwnerId = mapper.Map<PlayerViewModel>(playerService.FindById(gameEvent.OwnerId));
            gameEve.OwnerId.Username = playerService.FindById(gameEve.OwnerId.Id).User.UserName;
           
            ViewData["valid"] = valid;
            return View(gameEve);

        }

        [HttpGet]
        public IActionResult GameEventAdd()
        {
            ViewData["GameNames"] = db.Games.Select(x => x.GameName).ToList();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GameEventAdd(GameEventViewModel gameEvent)
        {

            var gameEve = mapper.Map<GameEvent>(gameEvent);
            var game = gameService.FindGameByName(gameEvent.GameName);
            gameEve.Game = game;
            var player = await playerService.AddAsync(new Player() { User = userService.FindUserByName(User.Identity.Name), UsernameInGame = gameEvent.OwnerId });
            gameEve.OwnerId = player.Id;

            player.GameEventsOwn.Add(gameEve);

            await gameEventService.AddAsync(gameEve);
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> GameEventDelete(GameEventViewModel gameEvent)
        {
            await gameEventService.DeleteEventAsync(gameEvent.Id);
            return RedirectToAction("Home", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> GameEventAddPlayer(string userNick, Guid gameEventId)
        {
            object obj = new { id = gameEventId };
            var playerInGameEvent = gameEventService.FindPlayerByNick(userNick, gameEventId);

            var gameEvent = gameEventService.FindEventById(gameEventId);
            var owner = playerService.FindById(gameEvent.OwnerId);

            if (playerInGameEvent == null)
            {
                Player playerNew = new Player() { User = userService.FindUserByName(User.Identity.Name), UsernameInGame = userNick };
                await gameEventService.AddPlayerAsync(playerNew, gameEventId);
                var notification = new Notification()
                {

                    Message = "Player " + userNick + " wants to join your event.",
                    SenderId = playerNew.User.Id,
                    RecipientId = owner.User.Id,
                    GameEvent = gameEvent,
                    IsRead = false

                };

                var curNotification = await userService.AddNotificationAsync(notification, owner.User.Id);

                //------------------------------------------SendNotificationTo(roomid)
                var ownerId = db.GameEvents.FirstOrDefault(x => x.Id == gameEventId).OwnerId;
                var player = db.Players.FirstOrDefault(x => x.Id == ownerId);
                var list = player.User.NotificationsRecived;
                await hub.Clients.User(ConnectionIdProvider.ids[player.User.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.OrderByDescending(x => x.CreatedAt).ToArray(),
                    NotCount = player.User.NotificationsRecived.Count(n => n.IsRead == false)
                });
                //----------------------------------------

                return RedirectToAction("GameEventDetail", obj);
            }
            else
            {
                obj = new { id = gameEventId, valid = false };
                return RedirectToAction("GameEventDetail", obj);
            }

        }

        public async Task<IActionResult> Accept(string playerName, Guid roomId)
        {
            object obj = new { id = roomId };
            var playerInGameEvent = gameEventService.FindPlayerByNick(playerName, roomId);

            var gameEvent = gameEventService.FindEventById(roomId);
            var owner = playerService.FindById(gameEvent.OwnerId);

            if (playerInGameEvent != null)
            {

                await playerService.ChangeStatusAsync(playerName, true);
                var notification = new Notification()
                {
                    Message = "Accepted by game event owner: " + owner.UsernameInGame,
                    SenderId = owner.User.Id,
                    RecipientId = playerInGameEvent.User.Id,
                    GameEvent = gameEvent,
                    IsRead = false
                };
                var curNotification = await userService.AddNotificationAsync(notification, playerInGameEvent.User.Id);

                var list = playerInGameEvent.User.NotificationsRecived;
                await hub.Clients.User(ConnectionIdProvider.ids[playerInGameEvent.User.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.ToArray(),
                    NotCount = playerInGameEvent.User.NotificationsRecived.Count(n => n.IsRead == false)
                });

                

                return RedirectToAction("GameEventDetail", obj);
            }
            else
            {
                obj = new { id = roomId, valid = false };
                return RedirectToAction("GameEventDetail", obj);
            }

        }

        public async Task<IActionResult> Decline(string playerName, Guid roomId)
        {
            object obj = new { id = roomId };
            var playerInGameEvent = gameEventService.FindPlayerByNick(playerName, roomId);

            var gameEvent = gameEventService.FindEventById(roomId);
            var owner = playerService.FindById(gameEvent.OwnerId);

            if (playerInGameEvent != null)
            {

                if (gameEvent.Players.Remove(playerInGameEvent))
                {
                    var notification = new Notification()
                    {
                        Message = "Declined by game event owner: " + owner.UsernameInGame,
                        SenderId = owner.User.Id,
                        RecipientId = playerInGameEvent.User.Id,
                        GameEvent = gameEvent,
                        IsRead = false
                    }; 
                     await userService.AddNotificationAsync(notification, playerInGameEvent.User.Id);
                }
                var list = playerInGameEvent.User.NotificationsRecived;
                await hub.Clients.User(ConnectionIdProvider.ids[playerInGameEvent.User.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.ToArray(),
                    NotCount = playerInGameEvent.User.NotificationsRecived.Count(n => n.IsRead == false)
                });
                return RedirectToAction("GameEventDetail", obj);
            }
            else
            {
                obj = new { id = roomId, valid = false };
                return RedirectToAction("GameEventDetail", obj);
            }
        }
    }
}
