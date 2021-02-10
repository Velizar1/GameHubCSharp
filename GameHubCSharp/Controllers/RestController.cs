using AutoMapper;
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
        private readonly IMapper mapper;

        public RestController(ApplicationDbContext applicationDb,
            IPlayerService playerService,
            IGameEventService gameEventService,
            IMapper mapper)
        {
            this.applicationDb = applicationDb;
            this.playerService = playerService;
            this.gameEventService = gameEventService;
            this.mapper = mapper;
        }

        [HttpGet("/resource")]
        public IActionResult FindGames(string game,int page=1,int pageSize=9)
        {
          
            if (game == "All")
            {
                var list = gameEventService.FindAll(page, pageSize).ToList();
                var list2 = list.Select(x => {
                    var re = mapper.Map<HomeEventRestViewModel>(x);
                    re.OwnerName = playerService.FindPlayerById(x.OwnerId.ToString()).UsernameInGame;
                    re.ImageUrl = x.Game.ImageUrl;
                    re.TakenPlaces = x.NumberOfPlayers;
                    return re;
                    })
                    .ToList();
                return Ok(list2);
            }
            var events = gameEventService.FindEventsByGame(game,page,pageSize);
            List<HomeEventRestViewModel> games = new List<HomeEventRestViewModel>();
            if (events.Count == 0) return NotFound();
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
