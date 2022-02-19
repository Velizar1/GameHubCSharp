using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IGameEventService : IBaseService
    {
        public List<GameEvent> FindEventsByGameName(string gameName, int? pageNumber, int? pageSize);
        public GameEvent FindEventById(Guid id);
        public Task AddAsync(GameEvent gameEvent);
        public List<GameEvent> FindAll(int? pageNumber, int? pageSize);
        public List<GameEvent> FindAll();
        public Task DeleteEventAsync(Guid id);
        public Task AddPlayerAsync(Player player, Guid gameEventId);
        public Player FindPlayerByNick(string userNick, Guid gameEventId);
        public Task DeleteAllExpiredGameEventsAsync();
    }
}