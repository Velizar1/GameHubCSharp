using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.DAL.Data.Models
{
    public class Category : BaseModel
    {

        public string Type { get; set; }

        public virtual ICollection<Post> Posts { get ; set ; }

        public Category()
        {
            Posts = new List<Post>();
        }
    }
}
