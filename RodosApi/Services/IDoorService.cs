using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Services
{
    public interface IDoorService
    {
        public Task<List<Door>> GetDoors(DoorFilter doorFilter, PaginationFilter filter = null, DoorSorting doorSorting = null);
        public Task<Door> GetDoor(long id);
        public Task<bool> CreateDoor(Door doorToCreate);
        public Task<bool> CreateDoors(List<Door> doorsToCreate);
        public Task<bool> UpdateDoor(Door doorToUpdate);
        public Task<bool> DeleteDoor(Door doorToDelete);
        public Task<bool> ValidationDoor(string name, long? id = null);
    }
}
