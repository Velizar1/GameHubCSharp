using AutoMapper;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using GameHubCSharp.BL.Models.DTO;
using GameHubCSharp.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
{

    public class GameEventService : IGameEventService
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext db;
        private readonly IPlayerService playerService;
        private readonly INotificationService notificationService;
        private readonly IRepository repository;

        public GameEventService(ApplicationDbContext db,
            IPlayerService playerService,
            IRepository repository,
            IMapper mapper = null,
            INotificationService notificationService = null
            )
        {
            this.db = db;
            this.playerService = playerService;
            this.mapper = mapper;
            this.notificationService = notificationService;
            this.repository = repository;
        }

        public async Task AddAsync(GameEvent gameEvent)
        {
            await repository.CreateAsync(gameEvent);
            await repository.SaveChangesAsync();
        }

        public async Task AddAsync(Player playerNew, string gameEventId)
        {
            var gameEvent =  this.FindEventById(gameEventId);
            gameEvent.Players.Add(playerNew);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAllExpiredGameEventsAsync()
        {
            var now = DateTime.Now;
            var list = repository.All<GameEvent>(g => g.DueDate <= now);
            db.GameEvents.RemoveRange(list);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(GameEventViewModel gameEvent)
        {
            var gameEve = repository
                .All<GameEvent>()
                .FirstOrDefault(g=>gameEvent.Id==g.Id);
            foreach (var player in gameEve.Players)
            {
                await repository.DeleteAsync(player);
            }
            var notifications = this.notificationService.GetForEvent(gameEve);
            foreach (var nott in notifications)
            {
                await repository.DeleteAsync(nott);
            }
            await repository.DeleteAsync(gameEve);
            await repository.SaveChangesAsync();
        }

        public ICollection<GameEvent> FindAll()
        {
            return repository
                .All<GameEvent>()
                .ToList();
        }

        public ICollection<GameEvent> FindAll(int page, int pageSize)
        {
            return repository
                .All<GameEvent>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public ICollection<GameEvent> FindEventsByGame(string game)
        {
           return repository
                .All<GameEvent>()
                .Where(g => g.Game.GameName == game)
                .ToList();
        }

        public ICollection<GameEvent> FindEventsByGame(string game, int page, int pageSize)
        {
            return repository
                .All<GameEvent>()
                .Where(g => g.Game.GameName == game)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public GameEvent FindEventById(string id)
        {
            return repository
                .All<GameEvent>()
                .FirstOrDefault(g => g.Id.ToString() == id);
        }

        public Player FindPlayerByNick(string userNick, string gameEventId)
        {
            return repository
                .All<GameEvent>()
                .FirstOrDefault(g => g.Id.ToString() == gameEventId)
                .Players
                .FirstOrDefault(p => p.UsernameInGame == userNick);
        }
    }
}
