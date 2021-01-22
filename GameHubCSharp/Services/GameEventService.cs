using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{

    public class GameEventService : IGameEventService<GameEvent>
    {
        private readonly ApplicationDbContext dbContext;
        readonly PlayerService playerService;

        public GameEventService(ApplicationDbContext db, PlayerService playerService)
        {
            this.dbContext = db;
            this.playerService = playerService;
        }

        public ICollection<GameEvent> FindEventsByGame(string game)
        {
           return dbContext.GameEvents.Where(g => g.Game.GameName == game).ToList();
        }

        public GameEvent FindEventsById(string id)
        {
            return dbContext.GameEvents.Where(g => g.Id.ToString() == id).First();
        }
    }
}
