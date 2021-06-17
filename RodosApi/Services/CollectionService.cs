using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class CollectionService:ICollectionService
    {
        private readonly ApplicationDbContext _dbContext;

        public CollectionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Collection>> GetAllCollections(PaginationFilter pagination=null,CollectionSorting collectionSorting = null,string name = null)
        {
            var queryable = _dbContext.Collections.AsQueryable();
            if(pagination == null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable,name);
            queryable = GetSorted(queryable,collectionSorting);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            return await queryable.Skip(skip).Take(pagination.PageSize).ToListAsync();
        }

       

        public async Task<Collection> GetCollection(long id)
        {
            return await _dbContext.Collections.FindAsync(id);
        }

        public async Task<bool> CreateCollection(Collection collectionToCreate)
        {
            await _dbContext.Collections.AddAsync(collectionToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateCollection(Collection collectionToUpdate)
        {
            _dbContext.Update(collectionToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteCollection(Collection collectionToDelete)
        {
            _dbContext.Collections.Remove(collectionToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> CollectionValidation(string name, long? id=null)
        {
            var coating = await _dbContext.Collections
                .Where(s => s.Name.ToLower().Trim() == name.ToLower().Trim() && s.CollectionId != id)
                .FirstOrDefaultAsync();
            if (coating != null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<Collection> GetFiltered(IQueryable<Collection> queryable, string name)
        {
            if(name != null)
            {
               queryable= queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }

        private IQueryable<Collection> GetSorted(IQueryable<Collection> queryable, CollectionSorting collectionSorting)
        {
            switch (collectionSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.CollectionId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.CollectionId);
                    break;
            }
            switch (collectionSorting.NameSort)
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
    }
}