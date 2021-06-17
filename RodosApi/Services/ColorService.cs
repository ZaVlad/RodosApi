using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _dbContext;

        public ColorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Color>> GetAllColors(PaginationFilter filter = null,ColorSorting colorSorting = null,string name = null)
        {
            var queryable = _dbContext.Colors.AsQueryable();
            if (filter is null)
            {
              return   await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable,name);
            queryable = GetSorted(queryable,colorSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();
        }

        

        public async Task<Color> GetColor(long colorId)
        {
            return await _dbContext.Colors.FirstOrDefaultAsync(s => s.Id == colorId);
        }

        public async Task<bool> CreateColor(Color colorToCreate)
        {
            await _dbContext.Colors.AddAsync(colorToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateColor(Color colorToUpdate)
        {
            _dbContext.Colors.Update(colorToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteColor(Color colorToDelete)
        {
            _dbContext.Colors.Remove(colorToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationColor(string name, long? id = null)
        {
            var validation =await 
                _dbContext.Colors.Where(s => s.Name.Trim().ToLower() == name.Trim().ToLower() 
                                             && s.Id != id).FirstOrDefaultAsync();
            if (validation != null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<Color> GetSorted(IQueryable<Color> queryable, ColorSorting colorSorting)
        {
            switch (colorSorting.IdSort)
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
            switch (colorSorting.NameSort)
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

        private IQueryable<Color> GetFiltered(IQueryable<Color> queryable, string name)
        {
            if (name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }
    }
}