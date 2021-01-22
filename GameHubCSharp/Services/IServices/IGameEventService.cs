using GameHubCSharp.Data.Models;
using System.Collections.Generic;

namespace GameHubCSharp.Services
{
    public interface IGameEventService
    {
        public ICollection<GameEvent> FindEventsByGame(string game);

        public GameEvent FindEventsById(string id);
        void Add(GameEvent gameEvent);
    }
}