using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Contract.V1.Request;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class TypeOfDoorService : ITypeOfDoorService
    {
        private readonly ApplicationDbContext _dbContext;
        public TypeOfDoorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TypeOfDoor>> GetAllTypesOfDoor(PaginationFilter filter = null, TypeOfDoorSorting typeOfDoorSorting = null, string name = null)
        {
            var queryable = _dbContext.TypesOfDoors.AsQueryable();
            if (filter == null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable, name);
            queryable = GetSorted(queryable, typeOfDoorSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();


        }

   

        public async Task<TypeOfDoor> GetTypeOfDoor(long id)
        {
            return await _dbContext.TypesOfDoors.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> CreateTypeOfDoor(TypeOfDoor typeToCreate)
        {
              _dbContext.TypesOfDoors.Add(typeToCreate);

              return await _dbContext.SaveChangesAsync() >= 1 ? true : false;
        }

        public async Task<bool> UpdateTypeOfDoor( TypeOfDoor typeToUpdate)
        {
            _dbContext.TypesOfDoors.Update(typeToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteTypeOfDoor(TypeOfDoor typeOfDoor)
        {
            _dbContext.TypesOfDoors.Remove(typeOfDoor);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationTypeOfDoorName(string name,long? id = null)
        {
            var type = await _dbContext.TypesOfDoors
                .Where(s => s.Name.ToLower().Trim() == name.ToLower().Trim() && s.Id != id)
                .FirstOrDefaultAsync();
            if (type != null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<TypeOfDoor> GetSorted(IQueryable<TypeOfDoor> queryable, TypeOfDoorSorting typeOfDoorSorting)
        {
            switch (typeOfDoorSorting.IdSort)
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

            switch (typeOfDoorSorting.NameSort)
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

        private IQueryable<TypeOfDoor> GetFiltered(IQueryable<TypeOfDoor> queryable, string name)
        {
            if (name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }
    }
}