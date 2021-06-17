using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface IMakerService
    {
        public Task<List<Maker>> GetAllMakers(PaginationFilter filter = null, MakerSorting makerSorting = null, MakerFilter makerFilter=null);
        public Task<Maker> GetMaker(long id);
        public Task<bool> CreateMaker(Maker makerToCreate);
        public Task<bool> UpdateMaker(Maker makerToUpdate);
        public Task<bool> DeleteMaker(Maker makerToDelete);
        public Task<bool> ValidationMaker(string name, long? id = null);
    }
}