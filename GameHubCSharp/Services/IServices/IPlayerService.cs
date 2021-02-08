using GameHubCSharp.Data.Models;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{
    public interface IPlayerService
    {
        public Player FindPlayerById(string id);
        public Player Add(Player player);
        public Player FindPlayerByNick(string userNick);
        public Task<Player> ChangeStatusAsync(string name, bool status);
        public Player ChangeStatus(string name, bool status);
    }
}