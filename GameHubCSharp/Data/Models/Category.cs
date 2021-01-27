using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Data.Models
{
    public class Category : BaseModel
    {
        public string Type { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
