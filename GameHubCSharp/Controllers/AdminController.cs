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
        public IActionResult AdminHome(string addType = "Game", string deleteType = "User")
        {
            //set ViewData
            var events = gameEventService.FindAll();
            var users = userService.FindAll();
            var posts = postService.FindAll();
            var games = gameService.FindAll();
            var categories = categoryService.FindAll();

            ViewData["events"] = events;
            ViewData["users"] = users;
            ViewData["posts"] = posts;
            ViewData["games"] = games;
            ViewData["categories"] = categories;

            var model = new AdminViewModel() { Add = addType, Delete = deleteType };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid id, string addType = "Game", string deleteType = "User")
        {
            await DeleteAsync(userService, id, deleteType);
            return RedirectToAction("AdminHome", "Admin");
        }



        [HttpGet]
        public async Task<IActionResult> DeleteCategory(Guid id, string addType = "Game", string deleteType = "User")
        {
            await DeleteAsync(categoryService, id, deleteType);
            return RedirectToAction("AdminHome", "Admin", new { AddType = addType, DeleteType = deleteType });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEvent(Guid id, string addType = "Game", string deleteType = "User")
        {
            deleteType = deleteType.Split("Proxy")[0];
            var eventt = mapper.Map<GameEventViewModel>(gameEventService.FindEventById(id));
            if (eventt != null && eventt.Id != null)
                await gameEventService.DeleteAsync(eventt.Id);
            return RedirectToAction("AdminHome", "Admin", new { AddType = addType, DeleteType = deleteType });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGame(Guid id, string addType = "Game", string deleteType = "User")
        {
            await DeleteAsync(gameService, id, deleteType);
            return RedirectToAction("AdminHome", "Admin", new { AddType = addType, DeleteType = deleteType });
        }

        [HttpGet]
        public async Task<IActionResult> DeletePost(Guid id, string addType = "Game", string deleteType = "User")
        {
            await DeleteAsync(postService, id, deleteType);
            return RedirectToAction("AdminHome", "Admin", new { AddType = addType, DeleteType = deleteType });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AdminViewModel model)
        {
            await categoryService.AddAsync(mapper.Map<Category>(model.Category));
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AddGame(AdminViewModel model)
        {
            await gameService.AddAsync(mapper.Map<Game>(model.Game));
            return RedirectToAction("AdminHome", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(string setCat, AdminViewModel model)
        {

            model.Post.CategoryId = categoryService.FindByType(setCat).Id;
            model.Post.CreatorId = (await userManager.GetUserAsync(User)).Id;
            model.Post.CreatedAt = DateTime.Now;
            await postService.AddPostAsync(mapper.Map<Post>(model.Post));
            await postService.SaveChangesAsync();
            return RedirectToAction("AdminHome", "Admin");
        }


        public IActionResult RedirectTo(string url)
        {
            return RedirectPermanent(url);
        }

        public async Task DeleteAsync(dynamic service, Guid id, string deleteType)
        {
            deleteType = deleteType.Split("Proxy")[0];
            await userService.DeleteAsync(id);
        }
    }
}
