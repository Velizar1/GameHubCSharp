using GameHubCSharp.Data.Models;

namespace GameHubCSharp.Services
{
    public interface IUserService
    {
        public User FindUserById(string userId);
    }
}