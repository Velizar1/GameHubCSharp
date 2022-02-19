using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.DAL.Repositories.Interfaces;
using GameHubCSharp.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameHubCSharp.BL.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IRepository _repository) : base(_repository) { }

        public async Task AddAsync(Category category)
        {
            await repository.CreateAsync(category);
        }

        public async Task DeleteAsync(Category category)
        {
            await repository.DeleteAsync(category);
        }

        public List<Category> FindAll()
        {
            return repository.AllReadOnly<Category>()
                .ToList();
        }

        public Category FindById(Guid id)
        {
            return repository.AllReadOnly<Category>()
                .FirstOrDefault(x => x.Id == id);
        }

        public Category FindByType(string type)
        {
            return repository.AllReadOnly<Category>()
                .FirstOrDefault(x => x.Type == type);
        }

    }
}
