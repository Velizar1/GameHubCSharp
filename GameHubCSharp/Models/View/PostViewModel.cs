using GameHubCSharp.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Models.View
{
    public class PostViewModel
    {

        private string creatorId;
        private string text;
        private string imageUrl;
        private string link;
        private string topic;
        private DateTime createdAt;
        private string category;

        public string CreatorId { get => creatorId; set => creatorId = value; }
        public string Text { get => text; set => text = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public string Link { get => link; set => link = value; }
        public string Topic { get => topic; set => topic = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public string Category { get => category; set => category = value; }
    }
}
