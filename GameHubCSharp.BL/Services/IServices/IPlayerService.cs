using GameHubCSharp.DAL.Data.Models;
using System;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IPlayerService : IBaseService
    {
        public Task<Player> DeleteAsync(Guid playerId);
        public Player FindById(Guid id);
        public Task<Player> AddAsync(Player player);
        public Player FindPlayerByNick(string userNick);
        public Task<Player> ChangeStatusAsync(string name, bool status);
    }
}