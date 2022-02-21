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
    public class HomeService : BaseService, IHomeService
    {

        public HomeService(IRepository _repository) : base(_repository)
        {

        }

        public ICollection<Game> FindAllGames()
        {
            return repository.AllReadOnly<Game>()
                .ToList();
        }
    }
}
