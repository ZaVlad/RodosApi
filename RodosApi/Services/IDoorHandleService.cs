using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Services
{
    public interface IDoorHandleService
    {
        public Task<List<DoorHandle>> GetAllDoorHandles(DoorHandleFilter doorHandlesFilter = null, PaginationFilter filter = null, DoorHandlesSorting doorHandlesSorting = null);
        public Task<DoorHandle> GetDoorHandle(long id);
        public Task<bool> CreateDoorHandle(DoorHandle doorHandleToCreate);
        public Task<bool> UpdateDoorHandle(DoorHandle doorHandleToUpdate);
        public Task<bool> DeletedoorHandle(DoorHandle doorHandleToDelete);
        public Task<bool> ValidationdoorHandle(string name, long? id = null);
    }
}
