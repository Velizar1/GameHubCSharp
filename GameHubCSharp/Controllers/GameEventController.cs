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
        public IActionResult GameEventDetail(string id)
        {
            var gameEvent = gameEventService.FindEventsById(id);
            if (gameEvent == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(gameEvent);

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
            db.SaveChanges();
            return RedirectToAction("Home","Home");
        }
    }
}
