using System.Collections.Generic;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface ITypeOfHingesService
    {
        public Task<List<TypeOfHinge>> GetAllTypesOfHinges(PaginationFilter filter = null, TypeOfHingesSorting typeOfHingesSorting = null, string name = null);
        public Task<TypeOfHinge> GetTypeOfHinges(long id);
        public Task<bool> CreateTypeOfHinges(TypeOfHinge typeOfHingesToCreate);
        public Task<bool> UpdateTypeOfHinges(TypeOfHinge typeOfHingesToUpdate);
        public Task<bool> DeleteTypeOfHinges(TypeOfHinge typeOfHingesToDelete);

        public Task<bool> TypeOfHingesValidation(string name, long? id = null);
    }
}
