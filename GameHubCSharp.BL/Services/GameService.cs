using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameHubCSharp.BL.Services
{
    public class GameService : BaseService, IGameService
    {

        public GameService(IRepository _repository) : base(_repository)
        {     
        }

        public async Task AddAsync(Game game)
        {
            await repository.CreateAsync(game);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var game = FindGameById(id);
            var events = repository.AllReadOnly<GameEvent>()
                .Where(x => x.Game.Id == game.Id)
                .ToList();

            foreach (var _event in events)
            {
                await repository.DeleteAsync(_event);
            }

            //await repository.DeleteRangeAsync(events);
            await repository.DeleteAsync(game);
            await repository.SaveChangesAsync();
        }

        public List<Game> FindAll()
        {
            return repository.AllReadOnly<Game>()
                .ToList();
        }
        public List<SelectListItem> FindAllSelectList()
        {
            return repository.AllReadOnly<Game>()
                .Select(x => new SelectListItem()
                {
                    Text = x.Id.ToString(),
                    Value=x.GameName
                })
                .ToList();
        }
        public int FindAllCount()
        {
            return repository.AllReadOnly<Game>()
                .Count();
        }

        public Game FindGameById(Guid id)
        {
            return repository.AllReadOnly<Game>()
                .FirstOrDefault(g => g.Id == id);
        }

        public Game FindGameByName(string name)
        {
            return repository.AllReadOnly<Game>()
                .FirstOrDefault(x => x.GameName == name);
        }
    }
}
