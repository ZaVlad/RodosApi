using System.Collections.Generic;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface ICoatingService
    {
        public Task<List<Coating>> GetAllCoatings(PaginationFilter filter = null,CoatingSorting coatingSorting = null, string name = null);
        public Task<Coating> GetCoating(long id);
        public Task<bool> CreateCoating(Coating coatingToCreate);
        public Task<bool> UpdateCoating(Coating coatingToUpdate);
        public Task<bool> DeleteCoating(Coating coating);
        public Task<bool> ValidationCoating(string name, long? id = null);
    }
}