﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.DAL.Data.Models
{
    public class Post : BaseModel
    {

        public Guid CategoryId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public string Link { get; set; }

        [Required]
        public string Topic { get; set; }

        public DateTime? CreatedAt { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User Creator { get; set; }
    }
}
