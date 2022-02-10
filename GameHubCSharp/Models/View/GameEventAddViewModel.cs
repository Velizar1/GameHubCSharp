using AutoMapper;
using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Models.View
{
    public class GameEventAddViewModel
    {

        private String ownerName;
        private String description;
        private int numberOfPlayers;
        private String devision;
        private DateTime startDate;
        private DateTime dueDate;
        private string gameName;
        private string discordUrl;

        public string Description { get => description; set => description = value; }
        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
        public string Devision { get => devision; set => devision = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public string GameName { get => gameName; set => gameName = value; }
        
        public string OwnerName { get => ownerName; set => ownerName = value; }
        public string DiscordUrl { get => discordUrl; set => discordUrl = value; }

        public void MapToDataModel(IProfileExpression configuration)
        {
            configuration.CreateMap<GameEvent, GameEventAddViewModel>();
        }
    }
}
