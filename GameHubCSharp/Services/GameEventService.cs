using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{

    public class GameEventService : IGameEventService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPlayerService playerService;

        public GameEventService(ApplicationDbContext db, IPlayerService playerService)
        {
            this.dbContext = db;
            this.playerService = playerService;
        }

        public void Add(GameEvent gameEvent)
        {
            throw new NotImplementedException();
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
