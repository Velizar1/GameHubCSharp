using AutoMapper;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.BL.Services;
using GameHubCSharp.BL.Services.IServices;
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
        private readonly ApplicationDbContext db;

        public AdminController(IUserService userService, IGameEventService gameEventService, IHomeService homeService, IPostService postService,
            IGameService gameService,
            IMapper mapper,
            ICategoryService categoryService,
            UserManager<User> userManager,
            ApplicationDbContext db)
        {
            this.userService = userService;
            this.gameEventService = gameEventService;
            this.homeService = homeService;
            this.postService = postService;
            this.gameService = gameService;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.userManager = userManager;
            this.db = db;
        }
        public IActionResult AdminHome(string addType = "Game",string deleteType = "User")
        {
            var events = gameEventService.FindAll();
            var users = userService.FindAll();
            var posts = postService.FindAll();
            var games = gameService.FindAll();
            var categories = categoryService.FindAll();
            var model = new AdminViewModel() { Users = users, GameEvents = events.ToList(), Posts = posts, Games = games, Categories = categories };

            model.Add = addType;
            model.Delete = deleteType;



            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid id, string addType = "Game", string deleteType = "User")
        {
            deleteType = deleteType.Split("Proxy")[0];
            await userService.DeleteAsync(id);
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(Guid id, string addType = "Game", string deleteType = "User")
        {
            deleteType = deleteType.Split("Proxy")[0];
            await categoryService.DeleteAsync(id);
            return RedirectToAction("AdminHome", "Admin",new { AddType = addType , DeleteType = deleteType });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEvent(Guid id, string addType = "Game", string deleteType = "User")
        {
            deleteType = deleteType.Split("Proxy")[0];
            var eventt = mapper.Map<GameEventViewModel>(gameEventService.FindEventById(id));
            if (eventt != null && eventt.Id != null)
                await gameEventService.DeleteEventAsync(eventt.Id);
            return RedirectToAction("AdminHome", "Admin", new { AddType = addType, DeleteType = deleteType });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGame(Guid id, string addType = "Game", string deleteType = "User")
        {
            deleteType = deleteType.Split("Proxy")[0];
            await gameService.DeleteAsync(id);
            return RedirectToAction("AdminHome", "Admin", new { AddType = addType, DeleteType = deleteType });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePost(Guid id, string addType = "Game", string deleteType = "User")
        {
            deleteType = deleteType.Split("Proxy")[0];
            await postService.RemovePostByIdAsync(id);
            return RedirectToAction("AdminHome", "Admin", new { AddType = addType, DeleteType = deleteType });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AdminViewModel model)
        {
            await categoryService.AddAsync(model.Category);
            return RedirectToAction("AdminHome","Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AddGame(AdminViewModel model)
        {
            await gameService.AddAsync(mapper.Map<Game>(model.Game));
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(string setCat,AdminViewModel model)
        {

            model.Post.CategoryId = categoryService.FindByType(setCat);
            model.Post.Creator = userService.FindUserByName(User.Identity.Name);
            model.Post.CreatedAt = DateTime.Now;
            await postService.AddPostAsync(model.Post);
            return RedirectToAction("AdminHome", "Admin");
        }

    
        public IActionResult RedirectTo(string url)
        {
            return RedirectPermanent(url);
        }

    }
}
