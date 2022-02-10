using GameHubCSharp.DAL.Data.Models;
using System.Collections.Generic;

namespace GameHubCSharp.Services
{
    public interface IHomeService
    {
        public ICollection<Game> FindAllGames();
    }
}