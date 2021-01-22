using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{
    public class PlayerService : IPlayerService
    {
        private ApplicationDbContext dbContext;

        public PlayerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Player FindPlayerById(string id)
        {
            var player = dbContext.Players.Where(p => p.Id.ToString() == id).First();
            return player;
        }
    }
}
