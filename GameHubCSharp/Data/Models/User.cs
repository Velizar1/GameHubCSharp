using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Data.Models
{
    public class User : IdentityUser
    {
        private Boolean deleted;

        public bool Deleted { get => deleted; set => deleted = value; }
    }
}
