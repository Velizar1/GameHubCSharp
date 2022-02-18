using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IGameService : IBaseService
    {
        public Game FindGameByName(string name);
        public Game FindGameById(string id);
        public Task AddAsync(Game game);
        public Task DeleteAsync(string id);
        public List<Game> FindAll();

    }
}
