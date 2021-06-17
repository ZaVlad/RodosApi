using System.Collections.Generic;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategory(string name =null,PaginationFilter filter = null,CategorySorting categorySorting = null);
        public Task<Category> GetCategory(long id);
        public Task<bool> CreateCategory(Category categoryToCreate);
        public Task<bool> UpdateCategory(Category categoryToUpdate);
        public Task<bool> DeleteCategory(Category categoryToDelete);
        public Task<bool> ValidationCategory(string name, long? id = null);

    }
}