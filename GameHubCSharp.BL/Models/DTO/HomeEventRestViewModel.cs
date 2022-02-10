using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Models.DTO
{
    public class HomeEventRestViewModel
    {

        private String id;
        private string imageUrl;
        private string ownerName;
        private int takenPlaces;
        private String devision;
        private DateTime startDate;

        public string Id { get => id; set => id = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public string OwnerName { get => ownerName; set => ownerName = value; }
        public int TakenPlaces { get => takenPlaces; set => takenPlaces = value; }
        public string Devision { get => devision; set => devision = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
    }
}
