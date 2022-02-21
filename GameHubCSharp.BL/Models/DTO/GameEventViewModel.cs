using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;


namespace GameHubCSharp.BL.Models.DTO
{
    public class GameEventViewModel 
    {
        public Guid Id { get ; set; }
        public Guid GameId { get; set; }
        public Guid OwnerId { get; set; }
        public string Description { get ; set ; }
        public int NumberOfPlayers { get ; set ; }
        public string Devision { get ; set ; }
        public DateTime StartDate { get ; set ; }
        public DateTime DueDate { get; set ; }
        public string DiscordUrl { get ; set ; }

    }
}
