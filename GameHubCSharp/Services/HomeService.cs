using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
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
