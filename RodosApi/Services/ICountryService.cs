using System.Collections.Generic;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface ICountryService
    {
        public Task<List<Country>> GetAllCountry(PaginationFilter filter = null,CountrySorting countrySorting = null, string name = null);
        public Task<Country> GetCountry(long countryId);
        public Task<bool> CreateCountry(Country countryToCreate);
        public Task<bool> UpdateCountry(Country countryToUpdate);

        public Task<bool> DeleteCountry(Country countryToDelete);
        public Task<bool> ValidationCountry(string name,long? id = null);
    }
}