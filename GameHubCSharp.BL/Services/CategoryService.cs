using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHubCSharp.DAL.Repositories.Interfaces;

namespace GameHubCSharp.BL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;
        private readonly IRepository repository;

        public CategoryService(ApplicationDbContext db, IRepository repository)
        {
            this.db = db;
            this.repository = repository;
        }
        public async Task AddAsync(Category category)
        {
            await repository.CreateAsync(category);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var category = repository
                .All<Category>()
                .FirstOrDefault(x => x.Id.ToString() == id);

            await repository.DeleteAsync(category);
            await repository.SaveChangesAsync();
        }

        public List<Category> FindAll()
        {
            return repository
                .All<Category>()
                .ToList();
        }

        public Category FindById(string id)
        {
            return repository
                .All<Category>()
                .FirstOrDefault(x => x.Id.ToString() == id);
        }

        public Category FindByName(string type)
        {
            return repository
                .All<Category>()
                .FirstOrDefault(x => x.Type == type);
        }
    }
}
