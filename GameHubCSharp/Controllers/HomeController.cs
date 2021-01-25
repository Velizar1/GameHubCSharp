using GameHubCSharp.Data;
using GameHubCSharp.Models;
using GameHubCSharp.Services;
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

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {

            _logger = logger;
            this.homeService = homeService;
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
            var games = homeService.FindAllGames();
            if (games.Count == 0)
            {
                ViewData["GameNames"] = "";
            }
            else
            {
                // Chech logic 
                ViewData["mostPlayed"] = games.Select(x => x.GameName).GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
                ViewData["GameNames"] = games.Select(x => x.GameName).ToList() ;
            }
            
            return View();
        }

        public IActionResult News()
        {
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
