using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Models.DTO
{
    public class HomeEventRestViewModel
    {
        public Guid Id { get ; set ; }
        public string ImageUrl { get ; set ; }
        public string OwnerName { get ; set ; }
        public int TakenPlaces { get ; set ; }
        public string Devision { get ; set ; }
        public DateTime StartDate { get ; set ; }
    }
}
