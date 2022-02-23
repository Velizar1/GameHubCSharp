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
using Microsoft.AspNetCore.Identity;

namespace GameHubCSharp.Controllers
{
    public class GameEventController : Controller
    {
        private readonly IGameEventService gameEventService;
        private readonly IMapper mapper;
        private readonly IGameService gameService;
        private readonly IPlayerService playerService;
        private readonly IUserService userService;
        private readonly IHubContext<NotificationHub> hub;
        private readonly INotificationService notificationService;
        private readonly UserManager<User> userManager;

        public GameEventController(
            IGameEventService _gameEventService,
            IMapper _mapper,
            IGameService _gameService,
            IPlayerService _playerService,
            IUserService _userService,
            IHubContext<NotificationHub> _hub,
            INotificationService _notificationService,
            UserManager<User> _userManager)
        {
            gameEventService = _gameEventService;
            mapper = _mapper;
            gameService = _gameService;
            playerService = _playerService;
            userService = _userService;
            hub = _hub;
            notificationService = _notificationService;
            userManager = _userManager;
        }

        [HttpGet("/game/detail/")]
        public IActionResult GameEventDetail(Guid id, bool? valid = true)
        {
            var gameEvent = gameEventService.FindEventById(id);

            if (gameEvent == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var gameEventVM = mapper.Map<GameEventViewModel>(gameEvent);
           
            ViewData["valid"] = valid;
            return View(gameEventVM);
            
        }

        [HttpGet]
        public IActionResult GameEventAdd()
        {
            SetGameViewData();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GameEventAdd(GameEventViewModel gameEventVM)
        {

            var gameEvent = mapper.Map<GameEvent>(gameEventVM);

            var player = await playerService.AddAsync(new Player()
            {
                UserId = gameEventVM.OwnerId,
                UsernameInGame=gameEventVM.OwnerNickName,
            });

            gameEvent.OwnerId = player.Id;

            player.GameEventsOwn.Add(gameEvent);

            await playerService.SaveChangesAsync();
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> GameEventDelete(GameEventViewModel gameEvent)
        {
            await gameEventService.DeleteEventAsync(gameEvent.Id);
            await gameEventService.SaveChangesAsync();
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
                Player playerNew = new Player() { UserId = (await userManager.GetUserAsync(User)).Id, UsernameInGame = userNick };
                await gameEventService.AddPlayerAsync(playerNew, gameEventId);
                await playerService.SaveChangesAsync();
                var notification = new Notification()
                {
                    Message = "Player " + userNick + " wants to join your event.",
                    SenderId = playerNew.UserId,
                    RecipientId = owner.UserId,
                    GameEventId = gameEvent.Id,
                    IsRead = false
                };

                var curNotification = await userService.AddNotificationAsync(notification, owner.UserId);

                await userService.SaveChangesAsync();
                //------------------------------------------SendNotificationTo(roomid)

                var user = userService.FindUserById(owner.UserId);
                var list =  user.NotificationsRecived;

                await hub.Clients.User(ConnectionIdProvider.ids[user.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.OrderByDescending(x => x.CreatedAt).ToArray(),
                    NotCount = user.NotificationsRecived.Count(n => n.IsRead == false)
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
                var curNotification = await userService.AddNotificationAsync(notification, playerInGameEvent.UserId);

                var user = userService.FindUserById(playerInGameEvent.UserId);
                var list = user.NotificationsRecived;
                await hub.Clients.User(ConnectionIdProvider.ids[playerInGameEvent.User.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.ToArray(),
                    NotCount = user.NotificationsRecived.Count(n => n.IsRead == false)
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
                var user = userService.FindUserById(playerInGameEvent.UserId);
                var list = user.NotificationsRecived;
           
                await hub.Clients.User(ConnectionIdProvider.ids[playerInGameEvent.User.UserName]).SendAsync("ReceiveNotfication", new
                {
                    Notifications = list.ToArray(),
                    NotCount = user.NotificationsRecived.Count(n => n.IsRead == false)
                });
                return RedirectToAction("GameEventDetail", obj);
            }
            else
            {
                obj = new { id = roomId, valid = false };
                return RedirectToAction("GameEventDetail", obj);
            }
        }

        private void SetGameViewData()
        {
            ViewData["GameNames"] = gameService.FindAllSelectList();

        }
    }
}
