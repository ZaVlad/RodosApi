using System.Collections.Generic;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface IMaterialService
    {
        public Task<List<Material>> GetAllMaterial(PaginationFilter filter = null, MaterialSorting materialSorting = null, string name = null);
        public Task<Material> GetMaterial(long materialId);
        public Task<bool> CreateMaterial(Material materialToCreate);
        public Task<bool> UpdateMaterial(Material materialToUpdate);

        public Task<bool> DeleteMaterial(Material materialToDelete);
        public Task<bool> ValidationMaterial(string name, long? id = null);
    }
}