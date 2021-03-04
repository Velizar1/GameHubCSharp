using GameHubCSharp.Data;
using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{
    public class PlayerService : IPlayerService
    {
        private ApplicationDbContext db;

        public PlayerService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public Player Add(Player player)
        {
            db.Players.Add(player);
            //db.SaveChanges();

            return player;
        }

        public Player ChangeStatus(string name, bool status)
        {
            var player = db.Players.FirstOrDefault(p => p.UsernameInGame == name);
            player.Status = status;
            db.SaveChanges();
            return player;
        }

        public async Task<Player> ChangeStatusAsync(string name, bool status)
        {
            var player = db.Players.FirstOrDefault(p => p.UsernameInGame == name);
            player.Status = status;

            await db.SaveChangesAsync();
            return player;
        }

        public Player DeletePlayer(string playerId)
        {
            var player = db.Players.FirstOrDefault(p => p.Id.ToString()==playerId);

            db.Remove(player);
            db.SaveChanges();
            return player;
        }

        public Player FindPlayerById(string id)
        {
            var player = db.Players.FirstOrDefault(p => p.Id.ToString() == id);
            return player;
        }

        public Player FindPlayerByNick(string userNick)
        {
            return db.Players.Where(x => x.UsernameInGame == userNick).FirstOrDefault();
        }
    }
}
