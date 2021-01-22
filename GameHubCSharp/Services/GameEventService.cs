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
        private readonly ApplicationDbContext db;
        private readonly IPlayerService playerService;

        public GameEventService(ApplicationDbContext db, IPlayerService playerService)
        {
            this.db = db;
            this.playerService = playerService;
        }

        public void Add(GameEvent gameEvent)
        {
            db.GameEvents.Add(gameEvent);

            db.SaveChanges();
        }

        public ICollection<GameEvent> FindAll()
        {
            return db.GameEvents.ToList();
        }

        public ICollection<GameEvent> FindEventsByGame(string game)
        {
           return db.GameEvents.Where(g => g.Game.GameName == game).ToList();
        }

        public GameEvent FindEventsById(string id)
        {
            return db.GameEvents.FirstOrDefault(g => g.Id.ToString() == id);
        }
    }
}
