using GameHubCSharp.DAL.Data.Models;
using System;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IPlayerService
    {
        public Player DeletePlayer(Guid playerId);
        public Player FindPlayerById(Guid id);
        public Player Add(Player player);
        public Player FindPlayerByNick(string userNick);
        public Task<Player> ChangeStatusAsync(string name, bool status);
        public Player ChangeStatus(string name, bool status);
    }
}