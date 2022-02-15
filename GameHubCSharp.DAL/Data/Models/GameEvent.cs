using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GameHubCSharp.DAL.Data.Models
{
    public class GameEvent : BaseModel
    {
        public Guid OwnerId { get; set; }

        public Guid GameId { get; set; }

        public string DiscordUrl { get; set; }

        public string Description { get; set; }

        [Required]
        public int NumberOfPlayers { get ; set ; }

        public string Devision { get ; set ; }

        [Required]
        public DateTime StartDate { get ; set ; }

        [Required]
        public DateTime? DueDate { get ; set ; }

        [JsonIgnore]
        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get ; set; }

        [JsonIgnore]
        [ForeignKey(nameof(OwnerId))]
        public virtual Player Owner { get; set; }

        [JsonIgnore]
        public virtual ICollection<Player> Players { get ; set ; }

        public GameEvent()
        {
            Players = new List<Player>();
        }
    }
}