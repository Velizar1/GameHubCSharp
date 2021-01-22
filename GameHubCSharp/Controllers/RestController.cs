using GameHubCSharp.Data;
using GameHubCSharp.Models.View;
using GameHubCSharp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        private ApplicationDbContext applicationDb;
        private IPlayerService playerService;
        private IGameEventService gameEventService;

        public RestController(ApplicationDbContext applicationDb, IPlayerService playerService, IGameEventService gameEventService)
        {
            this.applicationDb = applicationDb;
            this.playerService = playerService;
            this.gameEventService = gameEventService;
        }

        [HttpGet("/resource")]
        public IActionResult FindGames(string game)
        {
            var events = gameEventService.FindEventsByGame(game);
            List<HomeEventRestViewModel> games = new List<HomeEventRestViewModel>();
            if (games.Count == 0) return NotFound();
            foreach (var el in events)
            {
                HomeEventRestViewModel homeEventRestView = new HomeEventRestViewModel();
                homeEventRestView.Devision = el.Devision;
                homeEventRestView.ImageUrl = el.Game.ImageUrl;
                homeEventRestView.Id = el.Id.ToString();
                homeEventRestView.OwnerName = playerService.FindPlayerById(el.OwnerId).UsernameInGame;
                homeEventRestView.TakenPlaces = el.NumberOfPlayers;
                games.Add(homeEventRestView);
            }
            return Ok(games);
        }
    }
}
