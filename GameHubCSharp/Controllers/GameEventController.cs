using AutoMapper;
using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Models.View;
using GameHubCSharp.Services;
using GameHubCSharp.Services.IServices;
using Microsoft.AspNetCore.Mvc;
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

        public GameEventController(ApplicationDbContext db, 
            IGameEventService gameEventService,
            IMapper mapper,
            IGameService gameService,
            IPlayerService playerService,
            IUserService userService)
        {
            this.db = db;
            this.gameEventService = gameEventService;
            this.mapper = mapper;
            this.gameService = gameService;
            this.playerService = playerService;
            this.userService = userService;
        }

        [HttpGet("/game/detail/")]
        public IActionResult GameEventDetail(string id, bool? valid=true)
        {
            var gameEvent = gameEventService.FindEventsById(id);
            if (gameEvent == null)
            {
                return RedirectToAction("Error","Home");
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
            ViewData["GameNames"] = db.Games.Select(x => x.GameName).ToList(); ;
            return View();
        }


        [HttpPost]
        public IActionResult GameEventAdd(GameEventAddViewModel gameEvent)
        {

            var gameEve = mapper.Map<GameEvent>(gameEvent);
            var game = gameService.FindGameByName(gameEvent.GameName);
            gameEve.Game = game;
            var player = playerService.Add(new Player() { User = userService.FindUserByName(User.Identity.Name),UsernameInGame = gameEvent.OwnerName});
            gameEve.OwnerId = player.Id.ToString();

            player.GameEvents.Add(gameEve);
            gameEventService.Add(gameEve);
            return RedirectToAction("Home","Home");
        }

        [HttpPost]
        public IActionResult GameEventDelete(GameEventViewModel gameEvent)
        {
            gameEventService.DeleteEvent(gameEvent);
            return RedirectToAction("Home", "Home");
        }
        [HttpPost]
        public IActionResult GameEventAddPlayer(string userNick,string gameEventId)
        {
            object obj = new { id = gameEventId};
            var playerInGameEvent = gameEventService.FindPlayerByNick(userNick,gameEventId);

            if (playerInGameEvent == null)
            {
                Player playerNew = new Player() { User = userService.FindUserByName(User.Identity.Name), UsernameInGame = userNick };
                gameEventService.AddPlayer(playerNew, gameEventId);

                return RedirectToAction("GameEventDetail", obj);
            }
            else
            {
                obj = new { id = gameEventId, valid = false };
                return RedirectToAction("GameEventDetail", obj);
            }
           
        }
    }
}
