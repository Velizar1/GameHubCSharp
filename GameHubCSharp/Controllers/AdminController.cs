using AutoMapper;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Models.View;
using GameHubCSharp.Services;
using GameHubCSharp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {


        private IUserService userService;
        private IGameEventService gameEventService;
        private IHomeService homeService;
        private IPostService postService;
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;

        public AdminController(IUserService userService, IGameEventService gameEventService, IHomeService homeService, IPostService postService,
            IGameService gameService,
            IMapper mapper,
            ICategoryService categoryService,
            UserManager<User> userManager)
        {
            this.userService = userService;
            this.gameEventService = gameEventService;
            this.homeService = homeService;
            this.postService = postService;
            this.gameService = gameService;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.userManager = userManager;
        }
        public IActionResult AdminHome()
        {
            var events = gameEventService.FindAll();
            var users = userService.FindAll();
            var posts = postService.FindAll();
            var games = gameService.FindAll();
            var categories = categoryService.FindAll();
            var model = new AdminHomeViewModel() { Users = users, GameEvents = events.ToList(), Posts = posts, Games = games, Categories = categories };

            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            userService.Delete(id);
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpGet]
        public IActionResult DeleteCategory(string id)
        {
            categoryService.Delete(id);
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpGet]
        public IActionResult DeleteEvent(string id)
        {
            var eventt = mapper.Map<GameEventViewModel>(gameEventService.FindEventsById(id));
            if (eventt != null && eventt.Id != null)
                gameEventService.DeleteEvent(eventt);
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpGet]
        public IActionResult DeleteGame(string id)
        {
            var eventt = mapper.Map<GameEventViewModel>(gameEventService.FindEventsById(id));
            if (eventt != null && eventt.Id != null)
                gameEventService.DeleteEvent(eventt);
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpPost]
        public IActionResult AddCategory(AdminHomeViewModel model)
        {
            categoryService.Add(model.Category);
            return RedirectToAction("AdminHome","Admin");
        }

        [HttpPost]
        public IActionResult AddGame(AdminHomeViewModel model)
        {
            gameService.Add(mapper.Map<Game>(model.GameViewModel));
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpPost]
        public IActionResult AddPost(string setCat,AdminHomeViewModel model)
        {

            model.Post.Category = categoryService.FindByName(setCat);
            model.Post.Creator = userService.FindUserByName(User.Identity.Name);
            model.Post.CreatedAt = DateTime.Now;
            postService.AddPost(model.Post);
            return RedirectToAction("AdminHome", "Admin");
        }

    
        public IActionResult RedirectTo(string url)
        {
            return RedirectPermanent(url);
        }

    }
}
