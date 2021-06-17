using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Country>> GetAllCountry(PaginationFilter filter = null, CountrySorting countrySorting = null, string name = null)
        {
            var queryable = _dbContext.Countries.AsQueryable();
            if (filter is null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable,name);
            queryable = GetSorted(queryable,countrySorting);
            
            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable.Skip(skip).Take(filter.PageSize).ToListAsync();
        }

       

        public async Task<Country> GetCountry(long countryId)
        {
            return await _dbContext.Countries.FirstOrDefaultAsync(s => s.CountryId == countryId);
        }

        public async Task<bool> CreateCountry(Country countryToCreate)
        {
            await _dbContext.Countries.AddAsync(countryToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateCountry(Country countryToUpdate)
        {
            _dbContext.Countries.Update(countryToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteCountry(Country countryToDelete)
        {
            _dbContext.Countries.Remove(countryToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationCountry(string name, long? id = null)
        {
            var countryValidation = await _dbContext.Countries.Where(s =>
                s.Name.Trim().ToLower() == name.ToLower().Trim()
                && s.CountryId != id).FirstOrDefaultAsync();
            if (countryValidation != null)
            {
                return false;
            }
            return true;
        }

        private IQueryable<Country> GetSorted(IQueryable<Country> queryable, CountrySorting countrySorting)
        {
            switch (countrySorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.CountryId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.CountryId);
                    break;
            }
            switch (countrySorting.NameSort)
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

        private IQueryable<Country> GetFiltered(IQueryable<Country> queryable, string name)
        {
            if(name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }
    }
}