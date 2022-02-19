using GameHubCSharp.BL.Services.IServices;
using GameHubCSharp.DAL.Repositories;
using GameHubCSharp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHubCSharp.BL.Services
{
    public abstract class BaseService : IBaseService
    {
        protected readonly IRepository repository;

        public BaseService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task SaveChangesAsync()
        {
            await repository.SaveChangesAsync();
        }
    }
}
