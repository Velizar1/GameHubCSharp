using AutoMapper;
using GameHubCSharp.Data;
using GameHubCSharp.Models;
using GameHubCSharp.Models.View;
using GameHubCSharp.Services;
using GameHubCSharp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly ICategoryService categorySevice;
        private readonly INotificationService notificationService;
        private readonly IUserService userService;
        private IMemoryCache _cache;
        private readonly int pageSize = 4;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService,
            IGameEventService gameEventService, IGameService gameService, IPostService postService, IMapper mapper, ICategoryService categorySevice, IMemoryCache cache, INotificationService notificationService, IUserService userService)
        {

            _logger = logger;
            this.homeService = homeService;
            this.gameEventService = gameEventService;
            this.gameService = gameService;
            this.postService = postService;
            this.mapper = mapper;
            this.categorySevice = categorySevice;
            _cache = cache;
            this.notificationService = notificationService;
            this.userService = userService;
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
            try { 
            ConnectionIdProvider.notifications = userService.FindAllNotifications(User.Identity.Name);
            }
            catch
            {

            }
            var gameEvents = gameEventService.FindAll();
            var games = gameService.FindAll();
            if (games.Count == 0)
            {
                ViewData["GameNames"] = new List<string>();
            }
            else if (gameEvents.Count != 0)
            {
                // Chech logic 
                ViewData["mostPlayed"] = gameEvents.Select(x => x).GroupBy(x => x.Game.GameName).OrderByDescending(x => x.Count()).First().Key; ;
                ViewData["GameNames"] = games.Select(x => x.GameName).ToList();
            }
            else
            {
                ViewData["mostPlayed"] = "No games found";
                ViewData["GameNames"] = games.Select(x => x.GameName).ToList();
            }

            return View(ConnectionIdProvider.ids);
        }

        [HttpGet]
        public IActionResult News(int? pageNumber, string categoryName, string currentFilter, string searchString)
        {
            var count = postService.Count(categoryName??"");
            List<PostViewModel> model;
            ViewData["Category"] = categoryName;
            model = postService.FindAll(pageNumber ?? 1, pageSize, (categoryName??"")).Select(p =>
            {
                var viewCat = mapper.Map<PostViewModel>(p);
                viewCat.Category = p.Category.Type;
                return viewCat;
            }).ToList();



            ViewData["PageNumber"] = pageNumber ?? 1;
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            ViewData["HasNext"] = (pageNumber ?? 1) < totalPages ? "" : "disabled";
            ViewData["HasPrev"] = (pageNumber ?? 1) > 1 ? "" : "disabled";
            ViewData["Categories"] = categorySevice.FindAll();
           
            return View(model);
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
