using GameHubCSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubCSharp.Services.IServices
{
    public interface ICategoryService
    {
        public void Add(Category category);
        public void Delete(string id);
        public List<Category> FindAll();
        public Category FindByName(string type);
        public Category FindById(string id);
    }
}
