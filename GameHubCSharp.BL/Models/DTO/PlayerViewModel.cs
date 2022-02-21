using System;
using System.Collections.Generic;

namespace GameHubCSharp.BL.Models.DTO
{
    public class PlayerViewModel
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GameEventId { get; set; }
        public string UsernameInGame { get; set ; }
        public bool Status { get ; set ; }
    }
}