using AutoMapper;
using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Models.View;
using GameHubCSharp.Services.IServices;
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
        private readonly INotificationService notificationService;

        public GameEventService(ApplicationDbContext db, IPlayerService playerService, IMapper mapper = null, INotificationService notificationService = null)
        {
            this.db = db;
            this.playerService = playerService;
            this.mapper = mapper;
            this.notificationService = notificationService;
        }

        public void Add(GameEvent gameEvent)
        {
            db.GameEvents.Add(gameEvent);

            db.SaveChanges();
        }

        public void AddPlayer(Player playerNew, string gameEventId)
        {
            var gameEvent =  this.FindEventsById(gameEventId);
            gameEvent.Players.Add(playerNew);
            db.SaveChanges();
        }

        public void DeleteAllExpiredGameEvents()
        {
            var now = DateTime.Now;
            var list = db.GameEvents.Where(g => g.DueDate <= now);
            db.GameEvents.RemoveRange(list);
            db.SaveChanges();
        }

        public void DeleteEvent(GameEventViewModel gameEvent)
        {
            var gameEve = db.GameEvents.FirstOrDefault(g=>gameEvent.Id==g.Id.ToString());
            foreach (var player in gameEve.Players)
            {
                db.Remove(player);
            }
            var notifications = this.notificationService.GetForEvent(gameEve);
            foreach (var nott in notifications)
            {
                db.Remove(nott);
            }
            db.Remove(gameEve);
            db.SaveChanges();
        }

        public ICollection<GameEvent> FindAll()
        {
            return db.GameEvents.ToList();
        }

        public ICollection<GameEvent> FindAll(int page, int pageSize)
        {
            return db.GameEvents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public ICollection<GameEvent> FindEventsByGame(string game)
        {
           return db.GameEvents.Where(g => g.Game.GameName == game).ToList();
        }

        public ICollection<GameEvent> FindEventsByGame(string game, int page, int pageSize)
        {
            return db.GameEvents.Where(g => g.Game.GameName == game).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public GameEvent FindEventsById(string id)
        {
            return db.GameEvents.FirstOrDefault(g => g.Id.ToString() == id);
        }

        public Player FindPlayerByNick(string userNick, string gameEventId)
        {
            return db.GameEvents.Where(g => g.Id.ToString() == gameEventId).FirstOrDefault().Players.Where(p => p.UsernameInGame == userNick).FirstOrDefault();
        }
    }
}
