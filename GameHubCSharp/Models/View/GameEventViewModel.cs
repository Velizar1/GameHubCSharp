using System;
using System.Collections.Generic;


namespace GameHubCSharp.Models.View
{
    public class GameEventViewModel
    {
        private String id;
        private String description;
        private int numberOfPlayers;
        private String devision;
        private DateTime startDate;
        private DateTime dueDate;
        private GameViewModel game;
        private PlayerViewModel owner;
        private ICollection<PlayerViewModel> players;

        public string Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
        public string Devision { get => devision; set => devision = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public GameViewModel Game { get => game; set => game = value; }
        
        public ICollection<PlayerViewModel> Players { get => players; set => players = value; }
        public PlayerViewModel Owner { get => owner; set => owner = value; }
    }
}
