using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using GameHubCSharp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext db;

        public GameService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(Game game)
        {
            db.Games.Add(game);
            db.SaveChanges();
        }

        public List<Game> FindAll()
        {
            return db.Games.ToList();
        }

        public Game FindGameByName(string name)
        {
            return db.Games.FirstOrDefault(x => x.GameName == name);
        }
    }
}
