using GameHubCSharp.DAL.Data;
using GameHubCSharp.DAL.Data.Models;
using GameHubCSharp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext db;

        public CategoryService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Add(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            var cat = db.Categories.FirstOrDefault(x => x.Id.ToString() == id);
            db.Categories.Remove(cat);
            db.SaveChanges();
        }

        public List<Category> FindAll()
        {
            return db.Categories.ToList();
        }

        public Category FindById(string id)
        {
            return db.Categories.FirstOrDefault(x => x.Id.ToString() == id);
        }

        public Category FindByName(string type)
        {
            return db.Categories.FirstOrDefault(x => x.Type == type);
        }
    }
}
