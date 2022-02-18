using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IGameEventService : IBaseService
    {
        public Task<List<GameEvent>> FindEventsByGameName(string gameName, int? pageNumber, int? pageSize);
        public Task<GameEvent> FindEventById(Guid id);
        public Task Add(GameEvent gameEvent);
        public Task<List<GameEvent>> FindAll(int? pageNumber, int? pageSize);
        public Task DeleteEvent(Guid id);
        public Task AddPlayer(Player player, Guid gameEventId);
        public Task<Player> FindPlayerByNick(string userNick, Guid gameEventId);
        public Task DeleteAllExpiredGameEvents();
    }
}