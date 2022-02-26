using AutoMapper;
using GameHubCSharp.BL.Helper;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.BL.Services.IServices;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.Hubs;
using GameHubCSharp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            return View(gameEvent);

        }

        [HttpGet]
        public IActionResult GameEventAdd()
        {
            GameEventHelper.SetGameViewData(ViewData, gameService);
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GameEventAdd(GameEventViewModel gameEventVM)
        {

            var gameEvent = mapper.Map<GameEvent>(gameEventVM);

            var player = await playerService.AddAsync(new Player()
            {
                UserId = gameEventVM.OwnerId,
                UsernameInGame = gameEventVM.OwnerNickName,
            });

            gameEvent.OwnerId = player.Id;

            player.GameEventsOwn.Add(gameEvent);

            await playerService.SaveChangesAsync();
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> GameEventDelete(GameEventViewModel gameEvent)
        {
            await gameEventService.DeleteAsync(gameEvent.Id);
            await gameEventService.SaveChangesAsync();
            return RedirectToAction("Home", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> JoinGameEvent(string userNick, Guid gameEventId)
        {
            object routeValues = new { id = gameEventId };
            var playerInGameEvent = gameEventService.FindPlayerByNick(userNick, gameEventId);

            var gameEvent = gameEventService.FindEventById(gameEventId);

            if (playerInGameEvent == null)
            {
                Player playerNew = new Player() { UserId = (await userManager.GetUserAsync(User)).Id, UsernameInGame = userNick };
                await gameEventService.AddPlayerAsync(playerNew, gameEventId);
                var notification = new Notification()
                {
                    Message = "Player " + userNick + " wants to join your event.",
                    SenderId = playerNew.UserId,
                    RecipientId = gameEvent.OwnerId,
                    GameEventId = gameEvent.Id,
                    IsRead = false
                };

                var curNotification = await userService.AddNotificationAsync(notification, gameEvent.OwnerId);

                await userService.SaveChangesAsync();
                await SendNotificationTo(gameEvent.OwnerId);

                return RedirectToAction("GameEventDetail", routeValues);
            }
            routeValues = new { id = gameEventId, valid = false };
            return RedirectToAction("GameEventDetail", routeValues);

        }

        public async Task<IActionResult> Accept(string playerName, Guid roomId)
        {
            object routeValues = new { id = roomId };
            var playerInGameEvent = gameEventService.FindPlayerByNick(playerName, roomId);

            var gameEvent = gameEventService.FindEventById(roomId);
            var owner = playerService.FindById(gameEvent.OwnerId);

            if (playerInGameEvent != null)
            {

                await playerService.ChangeStatusAsync(playerName, true);
                var notification = new Notification()
                {
                    Message = "Accepted by game event owner: " + owner.UsernameInGame,
                    SenderId = owner.UserId,
                    RecipientId = playerInGameEvent.UserId,
                    GameEvent = gameEvent,
                    IsRead = false
                };
                var curNotification = await userService.AddNotificationAsync(notification, playerInGameEvent.UserId);

                await SendNotificationTo(playerInGameEvent.UserId);
                return RedirectToAction("GameEventDetail", routeValues);
            }

            routeValues = new { id = roomId, valid = false };
            return RedirectToAction("GameEventDetail", routeValues);

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
                        SenderId = owner.UserId,
                        RecipientId = playerInGameEvent.UserId,
                        GameEvent = gameEvent,
                        IsRead = false
                    };
                    await userService.AddNotificationAsync(notification, playerInGameEvent.User.Id);
                }
                await SendNotificationTo(playerInGameEvent.UserId);

                return RedirectToAction("GameEventDetail", obj);
            }
            else
            {
                obj = new { id = roomId, valid = false };
                return RedirectToAction("GameEventDetail", obj);
            }
        }

        private async Task SendNotificationTo(Guid userId)
        {
            var user = userService.FindUserById(userId);
            var list = user.NotificationsRecived;
            await hub.Clients.User(ConnectionIdProvider.ids[user.UserName]).SendAsync("ReceiveNotfication", new
            {
                Notifications = list.ToArray(),
                NotCount = user.NotificationsRecived.Count(n => n.IsRead == false)
            });
        }

    }
}
