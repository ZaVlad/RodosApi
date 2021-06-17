using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RodosApi.Contract;
using RodosApi.Contract.V1.Request;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Contract.V1.Response;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using RodosApi.Helpers;
using RodosApi.Services;

namespace RodosApi.Controllers.V1
{
    public class FurnitureTypeController : Controller
    {
        private readonly IFurnitureTypeService _furnitureType;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public FurnitureTypeController( IMapper mapper, IUriService uriService, IFurnitureTypeService furnitureType)
        {
            _mapper = mapper;
            _uriService = uriService;
            _furnitureType = furnitureType;
        }

        [HttpGet(ApiRoutes.FurnitureType.GetFurnitureTypes)]
        public async Task<IActionResult> GetFurnitureTypes(PaginationQuery paginationQuery,FurnitureTypeSortingQuery furnitureTypeSortingQuery, string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<FurnitureTypeSorting>(furnitureTypeSortingQuery);
            var furnitureType = await _furnitureType.GetAllFurnitureTypes(pagination,sorting,name);
            var furnitureTypeResponse = _mapper.Map<List<FurnitureTypeResponse>>(furnitureType);
            if (pagination is null || pagination.PageSize < 1 || pagination.PageNumber < 1)
            {
                return Ok(furnitureTypeResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse<FurnitureTypeResponse>(_uriService, pagination, furnitureTypeResponse);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.FurnitureType.GetFurnitureType)]
        public async Task<IActionResult> GetFurnitureTypes(long furnitureTypeId)
        {
            var furnitureType = await _furnitureType.GetFurnitureType(furnitureTypeId);
            if (furnitureType is null)
            {
                return NotFound();
            }

            var collectionResponse = _mapper.Map<FurnitureTypeResponse>(furnitureType);
            return Ok(collectionResponse);
        }

        [HttpPost(ApiRoutes.FurnitureType.CreateFurnitureType)]
        public async Task<IActionResult> CreateFurnitureType([FromBody] FurnitureTypeToCreate furnitureTypeToCreate)
        {
            bool validationFurnitureType = await _furnitureType.ValidationFurnitureType(furnitureTypeToCreate.Name);
            if (!validationFurnitureType)
            {
                ModelState.AddModelError("", "Furniture Type with the same name is already exists");
                return Conflict(ModelState);
            }

            var furnitureType = _mapper.Map<FurnitureType>(furnitureTypeToCreate);
            if (await _furnitureType.CreateFurnitureType(furnitureType)== false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            string baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.FurnitureType.GetFurnitureType)
                .Replace("{furnitureTypeId}", furnitureType.FurnitureId.ToString());
            return Created(baseUri, furnitureType);
        }

        [HttpPut(ApiRoutes.FurnitureType.UpdateFurnitureType)]
        public async Task<IActionResult> UpdateFurnitureType(long furnitureTypeId,
            [FromBody] FurnitureTypeToUpdate furnitureTypeToUpdate)
        {
            var furnitureType = await _furnitureType.GetFurnitureType(furnitureTypeId);

            if (furnitureType is null)
            {
                return NotFound();
            }

            var validationFurnitureType = await _furnitureType.ValidationFurnitureType(furnitureTypeToUpdate.Name, furnitureTypeId);
            if (validationFurnitureType == false)
            {
                ModelState.AddModelError("", "Furniture Type with the same name is already exists");
                return Conflict(ModelState);
            }

            furnitureType.Name = furnitureTypeToUpdate.Name;

            if (await _furnitureType.UpdateFurnitureType(furnitureType) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return Ok(_mapper.Map<FurnitureTypeResponse>(furnitureType));
        }

        [HttpDelete(ApiRoutes.FurnitureType.DeleteFurnitureType)]
        public async Task<IActionResult> DeleteFurnitureType(long furnitureTypeId)
        {
            var furnitureType = await _furnitureType.GetFurnitureType(furnitureTypeId);
            if (furnitureType is null)
            {
                return NotFound();
            }

            if (await _furnitureType.DeleteFurnitureType(furnitureType) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return NoContent();
        }

        
    }
}