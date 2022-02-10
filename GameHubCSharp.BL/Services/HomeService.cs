using GameHubCSharp.BL.Services.IServices;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
{
    public class HomeService : IHomeService 
    {
        private readonly ApplicationDbContext dbContext;

        public HomeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<Game> FindAllGames()
        {
            var games = dbContext.Games.ToList();
            return games;
        }
    }
}
