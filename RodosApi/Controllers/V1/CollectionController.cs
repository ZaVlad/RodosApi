using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RodosApi.Contract;
using RodosApi.Contract.V1.Request;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Contract.V1.Response;
using RodosApi.Domain;
using RodosApi.Helpers;
using RodosApi.Services;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Controllers.V1
{
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public CollectionController(ICollectionService collectionService, IMapper mapper, IUriService uriService)
        {
            _collectionService = collectionService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(ApiRoutes.Collection.GetCollections)]
        public async Task<IActionResult> GetAllCollections(PaginationQuery paginationQuery,CollectionSortingQuery collectionSorting,string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<CollectionSorting>(collectionSorting);
            var collections = await _collectionService.GetAllCollections(pagination,sorting,name);
            var collectionsResponse = _mapper.Map<List<CollectionResponse>>(collections);

            if (pagination is null || pagination.PageSize < 1 || pagination.PageNumber < 1)
            {
                return Ok(collectionsResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse<CollectionResponse>(_uriService, pagination, collectionsResponse);

            return Ok(result);
        }
        [HttpGet(ApiRoutes.Collection.GetCollection)]
        public async Task<IActionResult> GetCollection(long collectionId)
        {
            var collection = await _collectionService.GetCollection(collectionId);
            if (collection is null)
            {
                return NotFound();
            }

            var collectionResponse = _mapper.Map<CollectionResponse>(collection);
            return Ok(collectionResponse);
        }

        [HttpPost(ApiRoutes.Collection.CreateCollections)]
        public async Task<IActionResult> CreateCoating([FromBody] CollectionToCreate collectionToCreate)
        {
            bool validationCoating = await _collectionService.CollectionValidation(collectionToCreate.Name);
            if (!validationCoating)
            {
                ModelState.AddModelError("", "Collection with the same name is already exists");
                return Conflict(ModelState);
            }

            var collection = _mapper.Map<Collection>(collectionToCreate);
            if (await _collectionService.CreateCollection(collection) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            string baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Coatings.GetCoating)
                .Replace("{collectionId}", collection.CollectionId.ToString());
            return Created(baseUri, collection);
        }

        [HttpPut(ApiRoutes.Collection.UpdateCollections)]
        public async Task<IActionResult> UpdateCoating(long collectionId, [FromBody] CollectionToUpdate collectionToUpdate)
        {
            var collection = await _collectionService.GetCollection(collectionId);

            if (collection is null)
            {
                return NotFound();
            }

            var validationCoating = await _collectionService.CollectionValidation(collectionToUpdate.Name, collectionId);
            if (validationCoating == false)
            {
                ModelState.AddModelError("", "Collection with the same name is already exists");
                return Conflict(ModelState);
            }

            collection.Name = collectionToUpdate.Name;

            if (await _collectionService.UpdateCollection(collection) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return Ok(_mapper.Map<CollectionResponse>(collection));
        }

        [HttpDelete(ApiRoutes.Collection.DeleteCollections)]
        public async Task<IActionResult> DeleteCoating(long collectionId)
        {
            var collection = await _collectionService.GetCollection(collectionId);
            if (collection is null)
            {
                return NotFound();
            }

            if (await _collectionService.DeleteCollection(collection) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return NoContent();
        }
    }
}
