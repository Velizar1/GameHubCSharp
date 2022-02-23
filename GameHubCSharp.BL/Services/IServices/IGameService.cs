using GameHubCSharp.DAL.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IGameService : IBaseService
    {
        public Game FindGameByName(string name);
        public Game FindGameById(Guid id);
        public Task AddAsync(Game game);
        public Task DeleteAsync(Guid id);
        public List<Game> FindAll();
        public List<SelectListItem> FindAllSelectList();
        public int FindAllCount();
    }
}
