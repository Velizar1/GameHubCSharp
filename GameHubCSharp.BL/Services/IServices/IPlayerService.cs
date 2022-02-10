using GameHubCSharp.DAL.Data.Models;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IPlayerService
    {
        public Player DeletePlayer(string playerId);
        public Player FindPlayerById(string id);
        public Player Add(Player player);
        public Player FindPlayerByNick(string userNick);
        public Task<Player> ChangeStatusAsync(string name, bool status);
        public Player ChangeStatus(string name, bool status);
    }
}