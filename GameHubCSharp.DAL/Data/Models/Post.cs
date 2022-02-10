using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.DAL.Data.Models
{
    public class Post : BaseModel
    {

        private User creator;
        private string text;
        private string imageUrl;
        private string link;
        private string topic;
        private DateTime createdAt;
        private Category category;
        //private Guid categoryId;

        [Required]
        public virtual User Creator { get => creator; set => creator = value; }
        [Required]
        public string Text { get => text; set => text = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public string Link { get => link; set => link = value; }
        [Required]
        public string Topic { get => topic; set => topic = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public virtual Category Category { get => category; set => category = value; }
      //  public virtual Guid CategoryId { get => categoryId; set => categoryId = value; }
    }
}
