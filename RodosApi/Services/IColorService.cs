using System.Collections.Generic;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface IColorService
    {
        public Task<List<Color>> GetAllColors(PaginationFilter filter = null, ColorSorting colorSorting = null, string name = null);
        public Task<Color> GetColor(long colorId);
        public Task<bool> CreateColor(Color colorToCreate);
        public Task<bool> UpdateColor(Color colorToUpdate);
        public Task<bool> DeleteColor(Color colorToDelete);
        public Task<bool> ValidationColor(string name, long? id = null);
    }
}