using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Services
{
    public interface IHingesService
    {
        public Task<List<Hinges>> GetHinges(GetAllHingesFilter sortingHinges = null ,PaginationFilter filter = null, HingesSorting hingesSorting = null);
        public Task<Hinges> GetHinge(long id);
        public Task<bool> CreateHinges(Hinges hingeToUpdate);
        public Task<bool> UpdateHinges(Hinges hingeToUpdate);
        public Task<bool> DeleteHinges(Hinges hingeToDelete);
        public Task<bool> ValidationHinges(string name, long? id = null);
    }
}
