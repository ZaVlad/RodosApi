using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class CoatingService:ICoatingService
    {
        private readonly ApplicationDbContext _dbContext;

        public CoatingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Coating>> GetAllCoatings(PaginationFilter filter = null,CoatingSorting coatingSorting=null,string name =null)
        {
            var queryable = _dbContext.Coatings.AsQueryable();
            if (filter == null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable, name);
            queryable = GetSorted(queryable,coatingSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();
        }



        public async Task<Coating> GetCoating(long id)
        {
            return await _dbContext.Coatings.FindAsync(id);
        }

        public async Task<bool> CreateCoating(Coating coatingToCreate)
        {
            await _dbContext.Coatings.AddAsync(coatingToCreate);
            return await _dbContext.SaveChangesAsync()>0?true:false;
        }

        public async Task<bool> UpdateCoating(Coating coatingToUpdate)
        {
            _dbContext.Update(coatingToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteCoating(Coating coating)
        {
            _dbContext.Coatings.Remove(coating);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> ValidationCoating(string name, long? id = null)
        {
            var coating = await _dbContext.Coatings
                .Where(s => s.Name.ToLower().Trim() == name.ToLower().Trim() && s.Id != id)
                .FirstOrDefaultAsync();
            if (coating != null)
            {
                return false;
            }

            return true;
        }
        private IQueryable<Coating> GetFiltered(IQueryable<Coating> queryable, string name)
        {
            if(name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }

        private IQueryable<Coating> GetSorted(IQueryable<Coating> queryable, CoatingSorting coatingSorting)
        {
            switch (coatingSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Id);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Id);
                    break;
            }
            switch (coatingSorting.NameSort)
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