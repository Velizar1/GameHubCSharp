using AutoMapper;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.BL.Services;
using GameHubCSharp.BL.Services.IServices;
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
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IGameService gameService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public RestController(ApplicationDbContext applicationDb,
            IPlayerService playerService,
            IGameEventService gameEventService,
            IPostService postService,
            IUserService userService,
            IGameService gameService,
            ICategoryService categoryService,
            IMapper mapper)
        {
            this.applicationDb = applicationDb;
            this.playerService = playerService;
            this.gameEventService = gameEventService;
            this.postService = postService;
            this.userService = userService;
            this.gameService = gameService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet("/findToDelete")]
        public IActionResult Find(Guid id, string type)
        {
            if (type == "GameEvent")
                return Ok(gameEventService.FindEventById(id));

            else if (type == "Game")
                return Ok(gameService.FindGameById(id));

            else if (type == "Post")
                return Ok(postService.FindPostById(id));

            else if (type == "Category")
                return Ok(categoryService.FindById(id));

            else if (type == "User")
                return Ok(userService.FindUserById(id));

            return NotFound();
        }

        [HttpGet("/resource")]
        public IActionResult FindGames(string game,int page=1,int pageSize=9)
        {
          
            if (game == "All")
            {
                var list = gameEventService.FindAll(page, pageSize).ToList();
                var list2 = list.Select(x => {
                    var re = mapper.Map<HomeEventRestViewModel>(x);
                    re.OwnerName = playerService.FindById(x.OwnerId).UsernameInGame;
                    re.ImageUrl = x.Game.ImageUrl;
                    re.TakenPlaces = x.NumberOfPlayers;
                    return re;
                    })
                    .ToList();
                return Ok(list2.OrderBy(x=>x.StartDate));
            }
            var events = gameEventService.FindEventsByGameName(game,page,pageSize);
            List<HomeEventRestViewModel> games = new List<HomeEventRestViewModel>();
            if (events.Count == 0) return NotFound();
            foreach (var el in events)
            {
                HomeEventRestViewModel homeEventRestView = new HomeEventRestViewModel();
                homeEventRestView.Devision = el.Devision;
                homeEventRestView.ImageUrl = el.Game.ImageUrl;
                homeEventRestView.Id = el.Id.ToString();
                homeEventRestView.OwnerName = playerService.FindById(el.OwnerId).UsernameInGame;
                homeEventRestView.TakenPlaces = el.NumberOfPlayers;
                homeEventRestView.StartDate = el.StartDate;
                games.Add(homeEventRestView);
            }
            return Ok(games.OrderBy(x => x.StartDate));
        }
    }
}
