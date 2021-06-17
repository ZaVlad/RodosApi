using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodosApi.Contract.V1.Request;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface ITypeOfDoorService
    {
        public Task<List<TypeOfDoor>> GetAllTypesOfDoor(PaginationFilter filter=null, TypeOfDoorSorting typeOfDoorSorting= null, string name = null);
        public Task<TypeOfDoor> GetTypeOfDoor(long id);
        public Task<bool> CreateTypeOfDoor(TypeOfDoor doorToCreate);
        public Task<bool> UpdateTypeOfDoor(TypeOfDoor doorToUpdate);
        public Task<bool> DeleteTypeOfDoor(TypeOfDoor typeOfDoor);
        public Task<bool> ValidationTypeOfDoorName(string name, long? id = null);
    }
}