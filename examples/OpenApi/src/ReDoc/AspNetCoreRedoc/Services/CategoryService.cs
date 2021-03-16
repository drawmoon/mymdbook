using System.Threading.Tasks;
using AspNetCoreRedoc.Entities;
using AspNetCoreRedoc.Services.Interfaces;

namespace AspNetCoreRedoc.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<Category[]> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> Post(Category category)
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> Put(Category category)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}