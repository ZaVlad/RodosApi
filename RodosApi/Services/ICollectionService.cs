using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public interface ICollectionService
    {
        public Task<List<Collection>> GetAllCollections(PaginationFilter pagination = null,CollectionSorting collectionSorting=null,string name =null);
        public Task<Collection> GetCollection(long id);
        public Task<bool> CreateCollection(Collection collectionToCreate);
        public Task<bool> UpdateCollection(Collection collectionToUpdate);
        public Task<bool> DeleteCollection(Collection collectionToDelete);

        public Task<bool> CollectionValidation(string name, long? id = null);
    }
}