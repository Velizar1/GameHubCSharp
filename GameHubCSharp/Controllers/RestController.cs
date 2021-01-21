using GameHubCSharp.Data;
using GameHubCSharp.Models.View;
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

        public RestController(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }

        [HttpGet("/resource")]
        public IActionResult FindGames(string game)
        {
            var events = applicationDb.GameEvents.Include(x=>x.Game).Where(x => x.Game.GameName == game).ToList();
            List<HomeEventRestViewModel> games = new List<HomeEventRestViewModel>();
            foreach(var el in events)
            {
                HomeEventRestViewModel homeEventRestView = new HomeEventRestViewModel();
                homeEventRestView.Devision = el.Devision;
                homeEventRestView.ImageUrl = el.Game.ImageUrl;
                homeEventRestView.Id = el.Id.ToString();
                homeEventRestView.OwnerName = el.OwnerId;
                homeEventRestView.TakenPlaces = el.NumberOfPlayers;
                games.Add(homeEventRestView);
            }
            return Ok(games);
        }
    }
}
