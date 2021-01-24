using GameHubCSharp.Data.Models;
using GameHubCSharp.Models.View;
using System.Collections.Generic;

namespace GameHubCSharp.Services
{
    public interface IGameEventService
    {
        public ICollection<GameEvent> FindEventsByGame(string game);
        public GameEvent FindEventsById(string id);
        public void Add(GameEvent gameEvent);
        public ICollection<GameEvent> FindAll();
        public void DeleteEvent(GameEventViewModel gameEvent);
        public void AddPlayer(Player playerNew, string gameEventId);
        public Player FindPlayerByNick(string userNick, string gameEventId);
    }
}