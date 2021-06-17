using System.Collections.Generic;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface IFurnitureTypeService
    {
        public Task<List<FurnitureType>> GetAllFurnitureTypes(PaginationFilter filter = null, FurnitureTypeSorting furnitureTypeSorting = null, string name = null);
        public Task<FurnitureType> GetFurnitureType(long furnitureTypeId);
        public Task<bool> CreateFurnitureType(FurnitureType furnitureToCreate);
        public Task<bool> UpdateFurnitureType(FurnitureType furnitureToUpdate);
        public Task<bool> DeleteFurnitureType(FurnitureType furnitureToDelete);
        public Task<bool> ValidationFurnitureType(string name, long? id = null);
    }
}