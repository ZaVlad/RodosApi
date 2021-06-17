using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class FurnitureTypeService : IFurnitureTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public FurnitureTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<FurnitureType>> GetAllFurnitureTypes(PaginationFilter filter = null, FurnitureTypeSorting furnitureTypeSorting = null, string name = null)
        {
            var queryable = _dbContext.FurnitureTypes.AsQueryable();
            if (filter == null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable, name);
            queryable = GetSorted(queryable, furnitureTypeSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();
        }

       

        public async Task<FurnitureType> GetFurnitureType(long furnitureTypeId)
        {
            return await _dbContext.FurnitureTypes.FirstOrDefaultAsync(s => s.FurnitureId == furnitureTypeId);
        }

        public async Task<bool> CreateFurnitureType(FurnitureType furnitureToCreate)
        {
            await _dbContext.FurnitureTypes.AddAsync(furnitureToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateFurnitureType(FurnitureType furnitureToUpdate)
        {
            _dbContext.FurnitureTypes.Update(furnitureToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> DeleteFurnitureType(FurnitureType furnitureToDelete)
        {
            _dbContext.FurnitureTypes.Remove(furnitureToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationFurnitureType(string name, long? id = null)
        {
            var furnitureType = await _dbContext.FurnitureTypes
                .Where(s => s.Name.ToLower().Trim() == name.ToLower().Trim() && s.FurnitureId != id)
                .FirstOrDefaultAsync();
            if (furnitureType != null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<FurnitureType> GetSorted(IQueryable<FurnitureType> queryable, FurnitureTypeSorting furnitureTypeSorting)
        {
            switch (furnitureTypeSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s=>s.FurnitureId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.FurnitureId);
                    break;
            }

            switch (furnitureTypeSorting.NameSort)
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

        private IQueryable<FurnitureType> GetFiltered(IQueryable<FurnitureType> queryable, string name)
        {
            if(name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }
    }
}