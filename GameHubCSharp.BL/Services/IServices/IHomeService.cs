using GameHubCSharp.DAL.Data.Models;
using System.Collections.Generic;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IHomeService : IBaseService
    {
        public ICollection<Game> FindAllGames();
    }
}