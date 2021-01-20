using System;
using System.Collections.Generic;

namespace GameHubCSharp.Data.Models
{
    public class GameEvent : BaseModel
    {
        private String description;
        private int numberOfPlayers;
        private String devision;
        private DateTime startDate;
        private DateTime dueDate;

        private Game game;
        private HashSet<Player> players;

        public string Description { get => description; set => description = value; }
        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
        public string Devision { get => devision; set => devision = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public Game Game { get => game; set => game = value; }
        public HashSet<Player> Players { get => players; set => players = value; }
    }
}