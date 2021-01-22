using GameHubCSharp.Data;
using GameHubCSharp.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Controllers
{
    public class GameEventController : Controller
    {
        private ApplicationDbContext applicationDb;

        public GameEventController(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }

        [HttpGet("/game/detail/")]
        public IActionResult GameEventDetail(string id)
        {
             var gameEvent=applicationDb.GameEvents.Include(x=>x.Game).Where(g => g.Id.ToString().Equals(id)).FirstOrDefault();

           
            
            return View();

        }
    }
}
