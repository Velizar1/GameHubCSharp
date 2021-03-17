using AutoMapper;
using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Hubs;
using GameHubCSharp.Models;
using GameHubCSharp.Models.View;
using GameHubCSharp.Services;
using GameHubCSharp.Services.IServices;
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
        public IActionResult GameEventDetail(string id, bool? valid = true)
        {
            var gameEvent = gameEventService.FindEventsById(id);
            if (gameEvent == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var gameEve = mapper.Map<GameEventViewModel>(gameEvent);
            gameEve.Owner = mapper.Map<PlayerViewModel>(playerService.FindPlayerById(gameEvent.OwnerId));
            gameEve.Owner.Username = playerService.FindPlayerById(gameEve.Owner.Id).User.UserName;
           
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
        public IActionResult GameEventAdd(GameEventAddViewModel gameEvent)
        {

            var gameEve = mapper.Map<GameEvent>(gameEvent);
            var game = gameService.FindGameByName(gameEvent.GameName);
            gameEve.Game = game;
            var player = playerService.Add(new Player() { User = userService.FindUserByName(User.Identity.Name), UsernameInGame = gameEvent.OwnerName });
            gameEve.OwnerId = player.Id.ToString();

            player.GameEvents.Add(gameEve);

            gameEventService.Add(gameEve);
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public IActionResult GameEventDelete(GameEventViewModel gameEvent)
        {
            gameEventService.DeleteEvent(gameEvent);
            return RedirectToAction("Home", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> GameEventAddPlayer(string userNick, string gameEventId)
        {
            object obj = new { id = gameEventId };
            var playerInGameEvent = gameEventService.FindPlayerByNick(userNick, gameEventId);

            var gameEvent = gameEventService.FindEventsById(gameEventId);
            var owner = playerService.FindPlayerById(gameEvent.OwnerId);

            if (playerInGameEvent == null)
            {
                Player playerNew = new Player() { User = userService.FindUserByName(User.Identity.Name), UsernameInGame = userNick };
                gameEventService.AddPlayer(playerNew, gameEventId);
                var notification = new Notification()
                {

                    Message = "Player " + userNick + " wants to join your event.",
                    From = playerNew.User.UserName,
                    To = owner.User.UserName,
                    GameEvent = gameEvent,
                    IsRead = false

                };

                var curNotification = userService.AddNotification(notification, owner.User.Id.ToString());

                //------------------------------------------SendNotificationTo(roomid)
                var ownerId = db.GameEvents.FirstOrDefault(x => x.Id.ToString() == gameEventId).OwnerId;
                var player = db.Players.FirstOrDefault(x => x.Id.ToString() == ownerId);
                var list = player.User.Notifications;
                await hub.Clients.User(ConnectionIdProvider.ids[player.User.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.OrderByDescending(x => x.CreatedAt).ToArray(),
                    NotCount = player.User.Notifications.Count(n => n.IsRead == false)
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

        public async Task<IActionResult> Accept(string playerName, string roomId)
        {
            object obj = new { id = roomId };
            var playerInGameEvent = gameEventService.FindPlayerByNick(playerName, roomId);

            var gameEvent = gameEventService.FindEventsById(roomId);
            var owner = playerService.FindPlayerById(gameEvent.OwnerId);

            if (playerInGameEvent != null)
            {

                await playerService.ChangeStatusAsync(playerName, true);
                var notification = new Notification()
                {
                    Message = "Accepted by game event owner: " + owner.UsernameInGame,
                    From = owner.User.UserName,
                    To = playerInGameEvent.User.UserName,
                    GameEvent = gameEvent,
                    IsRead = false
                };
                var curNotification = userService.AddNotification(notification, playerInGameEvent.User.Id.ToString());

                var list = playerInGameEvent.User.Notifications;
                await hub.Clients.User(ConnectionIdProvider.ids[playerInGameEvent.User.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.ToArray(),
                    NotCount = playerInGameEvent.User.Notifications.Count(n => n.IsRead == false)
                });

                

                return RedirectToAction("GameEventDetail", obj);
            }
            else
            {
                obj = new { id = roomId, valid = false };
                return RedirectToAction("GameEventDetail", obj);
            }

        }

        public async Task<IActionResult> Decline(string playerName, string roomId)
        {
            object obj = new { id = roomId };
            var playerInGameEvent = gameEventService.FindPlayerByNick(playerName, roomId);

            var gameEvent = gameEventService.FindEventsById(roomId);
            var owner = playerService.FindPlayerById(gameEvent.OwnerId);

            if (playerInGameEvent != null)
            {

                if (gameEvent.Players.Remove(playerInGameEvent))
                {
                    var notification = new Notification()
                    {
                        Message = "Declined by game event owner: " + owner.UsernameInGame,
                        From = owner.User.UserName,
                        To = playerInGameEvent.User.UserName,
                        GameEvent = gameEvent,
                        IsRead = false
                    }; 
                     userService.AddNotification(notification, playerInGameEvent.User.Id.ToString());
                }
                var list = playerInGameEvent.User.Notifications;
                await hub.Clients.User(ConnectionIdProvider.ids[playerInGameEvent.User.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.ToArray(),
                    NotCount = playerInGameEvent.User.Notifications.Count(n => n.IsRead == false)
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
