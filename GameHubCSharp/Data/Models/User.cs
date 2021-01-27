using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        private Boolean deleted;
        public bool Deleted { get => deleted; set => deleted = value; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
