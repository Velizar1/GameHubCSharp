using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameHubCSharp.Data.Models
{
    public class GameEvent : BaseModel
    {
        private String description;
        private int numberOfPlayers;
        private String devision;
        private DateTime startDate;
        private DateTime dueDate;

        private string ownerId;
        private Game game;
        [JsonIgnore]
        private ICollection<Player> players;

        public string Description { get => description; set => description = value; }

        [Required]
        public int NumberOfPlayers { get => numberOfPlayers; set => numberOfPlayers = value; }
        public string Devision { get => devision; set => devision = value; }
        [Required]
        public DateTime StartDate { get => startDate; set => startDate = value; }
        [Required]
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public virtual Game Game { get => game; set => game = value; }
        public virtual ICollection<Player> Players { get => players; set => players = value; }
        public string OwnerId { get => ownerId; set => ownerId = value; }
        public string DiscordUrl { get; set; }

        public GameEvent()
        {

            Players = new List<Player>();
        }

        public GameEvent(string player) : base()
        {
            OwnerId = player;
        }

    }
}