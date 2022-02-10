using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Data.Models
{
    public class Category : BaseModel
    {
       
        private ICollection<Post> posts;
        public string Type { get; set; }
        public virtual ICollection<Post> Posts { get => posts; set => posts = value; }
    }
}
