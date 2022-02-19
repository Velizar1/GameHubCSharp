using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.DAL.Repositories.Interfaces;

namespace GameHubCSharp.BL.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext db;
        private readonly IGameEventService gameEventService;
        private readonly IRepository repository;

        public GameService(ApplicationDbContext db,IGameEventService gameEventService, IRepository repository)
        {
            this.db = db;
            this.gameEventService = gameEventService;
            this.repository = repository;
        }

        public async Task AddAsync(Game game)
        {
            await repository.CreateAsync(game);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var game = FindGameById(id);
            var events = gameEventService
                .FindAll()
                .Where(x => x.Game.Id == game.Id);

            db.RemoveRange(events);
            await repository.DeleteAsync(game);
            await repository.SaveChangesAsync();
        }

        public List<Game> FindAll()
        {
            return repository
                .All<Game>()
                .ToList();
        }

        public Game FindGameById(string id)
        {
            return repository
                .All<Game>()
                .FirstOrDefault(g => g.Id.ToString() == id);
        }

        public Game FindGameByName(string name)
        {
            return repository
                .All<Game>()
                .FirstOrDefault(x => x.GameName == name);
        }
    }
}
