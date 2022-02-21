using System;

namespace GameHubCSharp.BL.Models.DTO
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string GameName { get; set; }
        public string ImageUrl { get; set ; }
    }
}