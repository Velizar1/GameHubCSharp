using AutoMapper;
using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{

    public class GameEventService : IGameEventService
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;
        private readonly IPlayerService playerService;

        public GameEventService(ApplicationDbContext db, IPlayerService playerService, IMapper mapper = null)
        {
            this.db = db;
            this.playerService = playerService;
            this.mapper = mapper;
        }

        public void Add(GameEvent gameEvent)
        {
            db.GameEvents.Add(gameEvent);

            db.SaveChanges();
        }

        public void DeleteEvent(GameEventViewModel gameEvent)
        {
            var gameEve = mapper.Map<GameEvent>(gameEvent);
            foreach (var player in gameEve.Players)
            {
                db.Remove(player);
            }
            db.Remove(gameEve);
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
