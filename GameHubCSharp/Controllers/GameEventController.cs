using GameHubCSharp.Data;
using GameHubCSharp.Models.View;
using GameHubCSharp.Services;
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
        private ApplicationDbContext applicationDb;
        private IGameEventService gameEventService;

        public GameEventController(ApplicationDbContext applicationDb, IGameEventService gameEventService)
        {
            this.applicationDb = applicationDb;
            this.gameEventService = gameEventService;
        }

        [HttpGet("/game/detail/")]
        public IActionResult GameEventDetail(string id)
        {
            var gameEvent = gameEventService.FindEventsById(id);
            if (gameEvent == null)
            {
                return View("Error.cshtml");
            }
            return View();

        }

        [HttpGet]
        public IActionResult GameEventAdd()
        {
            ViewData["GameNames"] = new List<string>() { "Pesho", "Ivan" };
            return View();
        }


        [HttpPost]
        public IActionResult GameEventAdd(GameEventAddViewModel gameEvent)
        {

           // gameEventService.Add();
           
            return View();
        }
    }
}
