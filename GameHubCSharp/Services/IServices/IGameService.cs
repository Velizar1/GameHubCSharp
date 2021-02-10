using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services.IServices
{
    public interface IGameService
    {
        public Game FindGameByName(string name);
        public Game FindGameById(string id);
        public void Add(Game game);
        public void Delete(string id);
        public List<Game> FindAll();

    }
}
