using AutoMapper;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using GameHubCSharp.BL.Models.DTO;
using System.Threading.Tasks;
using GameHubCSharp.DAL.Repositories.Interfaces;
using GameHubCSharp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameHubCSharp.BL.Services
{

    public class GameEventService : IGameEventService
    {
        private readonly IRepository repository;

        public GameEventService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<List<GameEvent>> FindEventsByGameName(string gameName, int? pageNumber, int? pageSize)
        {
            return await repository.AllReadOnly<GameEvent>()
                 .Include(x => x.Game)
                 .Where(x => x.Game.GameName == gameName)
                 .OrderBy(x => x.StartDate)
                 .Skip((pageNumber ?? 1 - 1) * pageSize ?? 1)
                 .Take(pageSize ?? 1)
                 .ToListAsync();
        }

        public async Task<GameEvent> FindEventById(Guid id)
        {
            return await repository.AllReadOnly<GameEvent>()
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(GameEvent gameEvent)
        {
            await repository.CreateAsync(gameEvent);
        }

        public async Task<List<GameEvent>> FindAll(int? pageNumber, int? pageSize)
        {
            return await repository.AllReadOnly<GameEvent>()
                .OrderBy(x => x.StartDate)
                .Skip((pageNumber ?? 1 - 1) * pageSize ?? 1)
                .Take(pageSize ?? 1)
                .ToListAsync();
        }

        public async Task DeleteEvent(Guid id)
        {
            var gameEvent = await repository.AllReadOnly<GameEvent>()
                  .FirstOrDefaultAsync(x => x.Id == id);
            await repository.DeleteAsync(gameEvent);
        }

        public async Task AddPlayer(Player player, Guid gameEventId)
        {
            var gameEvent = await repository.All<GameEvent>()
                .Include(x=>x.Players)
                .FirstOrDefaultAsync(x => x.Id == gameEventId);
            if (!gameEvent.Players.Contains(player))
            {
                gameEvent.Players.Add(player);
            }
        }

        public async Task<Player> FindPlayerByNick(string userNick, Guid gameEventId)
        {
            return await repository.AllReadOnly<GameEvent>()
                .Include(x => x.Players)
                .Where(x => x.Id == gameEventId)
                .Select(x => x.Players.FirstOrDefault(y => y.UsernameInGame.Equals(userNick)))
                .FirstOrDefaultAsync();
        }

        public async Task DeleteAllExpiredGameEvents()
        {

            var expiredEvents = repository.AllReadOnly<GameEvent>()
                .Where(g => g.DueDate <= DateTime.Now);
            await repository.DeleteRangeAsync(expiredEvents);

        }

        public async Task SaveChanges()
        {
            await repository.SavechangesAsync();
        }
    }
}
