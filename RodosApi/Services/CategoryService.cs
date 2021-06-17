using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class CategoryService :ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllCategory(string name = null,PaginationFilter filter = null,CategorySorting categorySorting =null)
        {
            var queryable = _dbContext.Categories.AsQueryable();
            if (filter is null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable,name);
            queryable = GetSorting(queryable, categorySorting);

            var skip = (filter.PageNumber - 1)*filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();
        }

      

        public async Task<Category> GetCategory(long id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(s => s.CategoryId == id);
        }

        public async Task<bool> CreateCategory(Category categoryToCreate)
        {
            await _dbContext.Categories.AddAsync(categoryToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> UpdateCategory(Category categoryToUpdate)
        {
            _dbContext.Categories.Update(categoryToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> DeleteCategory(Category categoryToDelete)
        {
            _dbContext.Categories.Remove(categoryToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationCategory(string name, long? id = null)
        {
            var category = await _dbContext.Categories.Where(s => s.Name.Trim().ToLower() == name.Trim().ToLower()
                                                                  && s.CategoryId != id).FirstOrDefaultAsync();
            if (category !=null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<Category> GetSorting(IQueryable<Category> queryable, CategorySorting categorySorting)
        {
            switch (categorySorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                   queryable = queryable.OrderBy(s => s.CategoryId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.CategoryId);
                    break;
            }
            switch (categorySorting.NameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Name);
                    break;
            }
            return queryable;
        }
        private IQueryable<Category> GetFiltered(IQueryable<Category> queryable, string name)
        {
            if(name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }
    }
}