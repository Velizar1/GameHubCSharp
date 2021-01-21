using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Models.Service
{
    public class GameServiceModel: BaseServiceModel
    {
        private string gameName;
        private string imageUrl;

        public string GameName { get => gameName; set => gameName = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}
