using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class TypeOfHingesService :ITypeOfHingesService
    {
        private readonly ApplicationDbContext _dbContext;
        public TypeOfHingesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TypeOfHinge>> GetAllTypesOfHinges(PaginationFilter filter = null, TypeOfHingesSorting typeOfHingesSorting = null, string name = null)
        {
            var queryable = _dbContext.TypesOfHinges.AsQueryable();
            if (filter == null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable, name);
            queryable = GetSorted(queryable, typeOfHingesSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();
        }

    

        public async Task<TypeOfHinge> GetTypeOfHinges(long id)
        {
            return await _dbContext.TypesOfHinges.FirstOrDefaultAsync(s => s.TypeOfHingeId == id);

        }

        public async Task<bool> CreateTypeOfHinges(TypeOfHinge typeOfHingesToCreate)
        {
            await _dbContext.TypesOfHinges.AddAsync(typeOfHingesToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateTypeOfHinges(TypeOfHinge typeOfHingesToUpdate)
        {
            _dbContext.TypesOfHinges.Update(typeOfHingesToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteTypeOfHinges(TypeOfHinge typeOfHingesToDelete)
        {
            _dbContext.TypesOfHinges.Remove(typeOfHingesToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> TypeOfHingesValidation(string name, long? id = null)
        {
            var typeOfHinges = await _dbContext.TypesOfHinges
                .Where(s => s.Name.ToLower().Trim() == name.ToLower().Trim() && s.TypeOfHingeId!= id)
                .FirstOrDefaultAsync();
            if (typeOfHinges != null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<TypeOfHinge> GetSorted(IQueryable<TypeOfHinge> queryable, TypeOfHingesSorting typeOfHingesSorting)
        {
            switch (typeOfHingesSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.TypeOfHingeId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.TypeOfHingeId);
                    break;
            }

            switch (typeOfHingesSorting.NameSort)
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

        private IQueryable<TypeOfHinge> GetFiltered(IQueryable<TypeOfHinge> queryable, string name)
        {
            if (name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }
    }
}