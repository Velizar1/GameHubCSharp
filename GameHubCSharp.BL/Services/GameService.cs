using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext db;
        private readonly IGameEventService gameEventService;

        public GameService(ApplicationDbContext db,IGameEventService gameEventService)
        {
            this.db = db;
            this.gameEventService = gameEventService;
        }

        public void Add(Game game)
        {
            db.Games.Add(game);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            var game = FindGameById(id);
            var events = gameEventService.FindAll().Where(x => x.Game.Id == game.Id);

            db.RemoveRange(events);
            db.Remove(game);
            db.SaveChanges();
        }

        public List<Game> FindAll()
        {
            return db.Games.ToList();
        }

        public Game FindGameById(string id)
        {
            return db.Games.FirstOrDefault(g => g.Id.ToString() == id);
        }

        public Game FindGameByName(string name)
        {
            return db.Games.FirstOrDefault(x => x.GameName == name);
        }
    }
}
