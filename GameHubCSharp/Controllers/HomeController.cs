﻿using AutoMapper;
using GameHubCSharp.Models;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.BL.Services;
using GameHubCSharp.BL.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.BL.Constants;
using Microsoft.AspNetCore.Identity;
using GameHubCSharp.DAL.Data.Models;

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
        private readonly UserManager<User> userManager;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService,
            IGameEventService gameEventService, IGameService gameService, IPostService postService, IMapper mapper,
            ICategoryService categorySevice, IMemoryCache cache, INotificationService notificationService,
            IUserService userService, UserManager<User> _userManager)
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
            userManager = _userManager;
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
        public async Task<IActionResult> Home()
        {
            try
            {
                //July
                ConnectionIdProvider.notifications = userService.FindAllNotificationsByUserId((await userManager.GetUserAsync(User)).Id);
            }
            catch
            {

            }

            var gameEventsCount = gameEventService.FindAllCount();
            var gamesCount = gameService.FindAllCount();

            SetGameViewData(gameEventsCount, gamesCount);

            return View(ConnectionIdProvider.ids);
        }

        [HttpGet]
        public IActionResult News(int? pageNumber, string categoryName, string currentFilter, string searchString)
        {
            var count = postService.Count(categoryName ?? "");
            List<PostViewModel> model;
            ViewData["Category"] = categoryName;
            model = postService.FindAll(pageNumber ?? PageConstants.DefautPageNumber, PageConstants.PageSize, (categoryName ?? "")).Select(p =>
              {
                  var viewCat = mapper.Map<PostViewModel>(p);
                  viewCat.CategoryId = p.Category.Id;
                  return viewCat;
              }).ToList();



            ViewData["PageNumber"] = pageNumber ?? PageConstants.DefautPageNumber;
            var totalPages = (int)Math.Ceiling(count / (double)PageConstants.PageSize);
            ViewData["HasNext"] = (pageNumber ?? PageConstants.DefautPageNumber) < totalPages ? "" : "disabled";
            ViewData["HasPrev"] = (pageNumber ?? PageConstants.DefautPageNumber) > PageConstants.DefautPageNumber ? "" : "disabled";
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

        private void SetGameViewData(long gameCount = 0, long gameEventsCount = 0)
        {
            ViewData["GameNames"] = new List<string>();
            ViewData["mostPlayed"] = "No games found";
            ViewData["totalPages"] = gameEventsCount;
            if (gameCount != 0)
            {
                if (gameEventsCount != 0)
                {
                    // Chech logic 
                    ViewData["mostPlayed"] = gameEventService.FindMostPlayedGame();
                }
                ViewData["GameNames"] = gameService.FindAll().Select(x => x.GameName).ToList();
            }

        }
    }
}
