using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Models.DTO
{
    public class AdminViewModel
    {
        public UserViewModel Admin { get; set; }
        public GameViewModel Game { get; set; }
        public PostViewModel Post { get; set; }
        public CategoryViewModel Category { get; set; }
        public string Add { get; set; }
        public string Delete { get; set; }
    }
}
