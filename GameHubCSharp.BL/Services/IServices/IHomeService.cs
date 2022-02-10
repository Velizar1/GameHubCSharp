using GameHubCSharp.DAL.Data.Models;
using System.Collections.Generic;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IHomeService
    {
        public ICollection<Game> FindAllGames();
    }
}