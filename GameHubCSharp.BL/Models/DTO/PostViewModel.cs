using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Models.DTO
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get ; set; }
        public Guid CategoryId { get; set; }
        public string Text { get ; set; }
        public string ImageUrl { get; set; }
        public string Link { get ; set ; }
        public string Topic { get; set ; }
        public DateTime CreatedAt { get ; set; }
        
    }
}
