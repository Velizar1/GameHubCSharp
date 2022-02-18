using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services.IServices
{
    public interface IBaseService
    {
        Task SaveChanges();
    }
}
