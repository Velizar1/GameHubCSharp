using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services.IServices
{
    public interface IGameService
    {
        public Game FindGameByName(string name);
        public void Add(Game game);

    }
}
