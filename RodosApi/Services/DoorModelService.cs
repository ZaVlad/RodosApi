using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class DoorModelService : IDoorModelService
    {
        private readonly ApplicationDbContext _dbContext;
        public DoorModelService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<DoorModel>> GetAllDoorModels(PaginationFilter filter = null, DoorModelSorting doorModelSorting = null, string name = null)
        {
            var queryable = _dbContext.DoorModels.AsQueryable();
            if (filter == null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable,name);
            queryable = GetSorted(queryable,doorModelSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();
        }

       
        public async Task<DoorModel> GetDoorModel(long doorModelId)
        {
            return await _dbContext.DoorModels.FirstOrDefaultAsync(s => s.Id == doorModelId);
        }

        public async Task<bool> CreateDoorModel(DoorModel doorModelToCreate)
        {
            await _dbContext.DoorModels.AddAsync(doorModelToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateDoorModel(DoorModel doorModelToUpdate)
        {
             _dbContext.DoorModels.Update(doorModelToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> DeleteDoorModel(DoorModel doorModelToDelete)
        {
             _dbContext.DoorModels.Remove(doorModelToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> ValidateDoorModel(string name, long? id = null)
        {
            var validation = await _dbContext.DoorModels
                .Where(s => s.Name.Trim().ToLower() == name.ToLower().Trim()
                            && s.Id != id).FirstOrDefaultAsync();
            if (validation !=null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<DoorModel> GetFiltered(IQueryable<DoorModel> queryable, string name)
        {
            if (name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }

        private IQueryable<DoorModel> GetSorted(IQueryable<DoorModel> queryable, DoorModelSorting doorModelSorting)
        {
            switch (doorModelSorting.IdSort)
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
            switch (doorModelSorting.NameSort)
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