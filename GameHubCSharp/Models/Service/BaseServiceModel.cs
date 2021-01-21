using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Models.Service
{
    public class BaseServiceModel
    {

        Guid Id;
        public Guid Id1 { get => Id; set => Id = value; }
    }
}
