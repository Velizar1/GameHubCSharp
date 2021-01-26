using AutoMapper;
using GameHubCSharp.Data;
using GameHubCSharp.Models;
using GameHubCSharp.Models.View;
using GameHubCSharp.Services;
using GameHubCSharp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHomeService homeService;
        private readonly IGameEventService gameEventService;
        private readonly IGameService gameService;
        private readonly IPostService postService;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService,
            IGameEventService gameEventService, IGameService gameService, IPostService postService, IMapper mapper)
        {

            _logger = logger;
            this.homeService = homeService;
            this.gameEventService = gameEventService;
            this.gameService = gameService;
            this.postService = postService;
            this.mapper = mapper;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Home");
            }
            return View();
        }
        [HttpGet("/home")]

        public IActionResult Home()
        {
            var gameEvents = gameEventService.FindAll();
            var games = gameService.FindAll();
            if (games.Count == 0)
            {
                ViewData["GameNames"] = new List<string>();
            }
            else if(gameEvents.Count!=0)
            {
                // Chech logic 
                ViewData["mostPlayed"] = gameEvents.Select(x => x).GroupBy(x => x.Game.GameName).OrderByDescending(x => x.Count()).First().Key; ;
                ViewData["GameNames"] = games.Select(x => x.GameName).ToList() ;
            }
            else
            {
                ViewData["mostPlayed"] = "No games found";
                ViewData["GameNames"] = games.Select(x => x.GameName).ToList();
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult News()
        {
            ViewData["Posts"] = postService.FindAll().Select(p=> mapper.Map<PostViewModel>(p)).ToList();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
