using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class MaterialService :IMaterialService
    {
        private readonly ApplicationDbContext _dbContext;

        public MaterialService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Material>> GetAllMaterial(PaginationFilter filter = null, MaterialSorting materialSorting = null, string name = null)
        {
            var queryable = _dbContext.Materials.AsQueryable();
            if (filter is null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable, name);
            queryable = GetSorted(queryable, materialSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();
        }

    

        public async Task<Material> GetMaterial(long materialId)
        {
            return await _dbContext.Materials.FirstOrDefaultAsync(s => s.MaterialId== materialId);
        }

        public async Task<bool> CreateMaterial(Material materialToCreate)
        {
            await _dbContext.Materials.AddAsync(materialToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateMaterial(Material materialToUpdate)
        {
            _dbContext.Materials.Update(materialToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteMaterial(Material materialToDelete)
        {
            _dbContext.Materials.Remove(materialToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationMaterial(string name, long? id = null)
        {
            var validation = await
                _dbContext.Materials.Where(s => s.Name.Trim().ToLower() == name.Trim().ToLower()
                                             && s.MaterialId != id).FirstOrDefaultAsync();
            if (validation != null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<Material> GetSorted(IQueryable<Material> queryable, MaterialSorting materialSorting)
        {
            switch (materialSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.MaterialId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.MaterialId);
                    break;
            }

            switch (materialSorting.NameSort)
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

        private IQueryable<Material> GetFiltered(IQueryable<Material> queryable, string name)
        {
            if(name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }
    }
}