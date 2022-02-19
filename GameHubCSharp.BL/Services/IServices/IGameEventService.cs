using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IGameEventService
    {
        public ICollection<GameEvent> FindEventsByGame(string game);
        public ICollection<GameEvent> FindEventsByGame(string game,int page,int pageSize);
        public GameEvent FindEventById(string id);
        public Task AddAsync(GameEvent gameEvent);
        public ICollection<GameEvent> FindAll();
        public ICollection<GameEvent> FindAll(int page,int pageSize);
        public Task DeleteAsync(GameEventViewModel gameEvent);
        public Task AddAsync(Player playerNew, string gameEventId);
        public Player FindPlayerByNick(string userNick, string gameEventId);
        public Task DeleteAllExpiredGameEventsAsync();
    }
}