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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository repository;

        public CategoryService(Repository _repository)
        {
            repository = _repository;
        }

        public async Task Add(Category category)
        {
            await repository.CreateAsync(category);
        }

        public async Task Delete(Category category)
        {
            await repository.DeleteAsync(category);
        }

        public async Task<List<Category>> FindAll()
        {
            return await repository.AllReadOnly<Category>()
                .ToListAsync();
        }

        public async Task<Category> FindById(Guid id)
        {
            return await repository.AllReadOnly<Category>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> FindByType(string type)
        {
            return await repository.AllReadOnly<Category>()
                .FirstOrDefaultAsync(x => x.Type == type);
        }

        public async Task SaveChanges()
        {
            int modifiedEntriesCount = await repository.SavechangesAsync();

            if (modifiedEntriesCount == 0)
            {
                //return number of saved entries
            }
        }
    }
}
