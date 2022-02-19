using GameHubCSharp.BL.Services.IServices;
using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
{
    public class PlayerService : BaseService, IPlayerService
    {

        public PlayerService(IRepository _repository) : base(_repository)
        {

        }

        public async Task<Player> AddAsync(Player player)
        {
            await repository.CreateAsync(player);
            await repository.SaveChangesAsync();

            return player;
        }

        public async Task<Player> ChangeStatusAsync(string name, bool status)
        {
            var player = repository
                .All<Player>()
                .FirstOrDefault(p => p.UsernameInGame == name);

            player.Status = status;

            await repository.SaveChangesAsync();
            return player;
        }

        public async Task<Player> DeleteAsync(Guid playerId)
        {
            var player = repository
                .All<Player>()
                .FirstOrDefault(p => p.Id == playerId);

            await repository.DeleteAsync(player);
            await repository.SaveChangesAsync();

            return player;
        }

        public Player FindById(Guid id)
        {
            var player = repository
                .All<Player>()
                .FirstOrDefault(p => p.Id == id);

            return player;
        }

        public Player FindPlayerByNick(string userNick)
        {
            return repository
                .All<Player>().
                FirstOrDefault(x => x.UsernameInGame == userNick);
        }
    }
}
