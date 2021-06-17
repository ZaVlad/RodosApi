using System.Collections.Generic;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface IDoorModelService
    {
        public Task<List<DoorModel>> GetAllDoorModels(PaginationFilter filter = null,DoorModelSorting doorModelSorting = null,string name = null);
        public Task<DoorModel> GetDoorModel(long doorModelId);
        public Task<bool> CreateDoorModel(DoorModel doorModelToCreate);
        public Task<bool> UpdateDoorModel(DoorModel doorModelToUpdate);
        public Task<bool> DeleteDoorModel(DoorModel doorModelToDelete);
        public Task<bool> ValidateDoorModel(string name, long? id = null);
    }
}