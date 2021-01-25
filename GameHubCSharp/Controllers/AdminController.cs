using GameHubCSharp.Services;
using GameHubCSharp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Controllers
{

    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {


        private IUserService userService;
        private IGameEventService gameEventService;
        private IHomeService homeService;
        private IPostService postService;

        public AdminController(IUserService userService, IGameEventService gameEventService, IHomeService homeService, IPostService postService)
        {
            this.userService = userService;
            this.gameEventService = gameEventService;
            this.homeService = homeService;
            this.postService = postService;
        }

        [Authorize(Roles ="User")]
        public IActionResult News()
        {
            return View();
        }
        [HttpGet("/home")]

        public IActionResult AdminHome()
        {
          
            return View();
        }

       
    }
}
