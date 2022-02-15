using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;


namespace GameHubCSharp.BL.Models.DTO
{
    public class GameEventViewModel 
    {
        private Guid id;
        private String description;
        private int numberOfPlayers;
        private String devision;
        private DateTime startDate;
        private DateTime dueDate;
        private string discordUrl;

        private GameViewModel game;
        private PlayerViewModel owner;
        private ICollection<PlayerViewModel> players;

        public Guid Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
        public string Devision { get => devision; set => devision = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public GameViewModel Game { get => game; set => game = value; }
        
        public ICollection<PlayerViewModel> Players { get => players; set => players = value; }
        public PlayerViewModel Owner { get => owner; set => owner = value; }
        public string DiscordUrl { get => discordUrl; set => discordUrl = value; }
    }
}
