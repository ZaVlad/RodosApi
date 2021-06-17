using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
    public class TypeOfDoorController : Controller
    {
        private readonly ITypeOfDoorService _typeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public TypeOfDoorController(ITypeOfDoorService typeService, IMapper mapper, IUriService uriService)
        {
            _typeService = typeService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(ApiRoutes.TypeOfDoors.GetTypes)]
        public async Task<IActionResult> GetTypes([FromQuery] PaginationQuery paginationQuery,TypeOfDoorSortingQuery typeOfDoorSortingQuery,string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<TypeOfDoorSorting>(typeOfDoorSortingQuery);

            var types = await _typeService.GetAllTypesOfDoor(pagination,sorting,name);
            var typesResponse = _mapper.Map<List<TypeOfDoorResponse>>(types);


            if (pagination is null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<TypeOfDoorResponse>(typesResponse));
            }

            var result =PaginationHelpers.CreatePaginationResponse<TypeOfDoorResponse>(_uriService, pagination, typesResponse);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.TypeOfDoors.GetTypeOfDoor)]
        public async Task<IActionResult> GetType(int typeId)
        {
            var type = await _typeService.GetTypeOfDoor(typeId);
            if (type is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TypeOfDoorResponse>(type));
        }

        [HttpPost(ApiRoutes.TypeOfDoors.CreateType)]
        public async Task<IActionResult> CreateType([FromBody] TypeOfDoorToCreate typeOfDoor)
        {
            var typeValidation =await  _typeService.ValidationTypeOfDoorName(typeOfDoor.Name);
            if (typeValidation is false)
            {
                ModelState.AddModelError("", "TypeOfDoor with the same name is already exists");
                return Conflict(ModelState);
            }

            var type = _mapper.Map<TypeOfDoor>(typeOfDoor);
            if (await _typeService.CreateTypeOfDoor(type) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            
            var baseUri = $"{_uriService.BaseUri()} {ApiRoutes.TypeOfDoors.CreateType.Replace("{typeId}", type.Id.ToString())}";


            return Created(new Uri(baseUri),type);
        }

        [HttpPut(ApiRoutes.TypeOfDoors.UpdateType)]
        public async Task<IActionResult> UpdateType([FromBody] TypeOfDoorToUpdate typeToUpdate, [FromRoute]int typeId)
        {
            var existTypeOfDoor =await _typeService.GetTypeOfDoor(typeId);
            if (existTypeOfDoor is null)
            {
                return NotFound();
            }

            if (await _typeService.ValidationTypeOfDoorName(typeToUpdate.Name, typeId) == false)
            {
                ModelState.AddModelError("", "TypeOfDoor with the same name is already exists");
                return Conflict(ModelState);
            }

            existTypeOfDoor.Name = typeToUpdate.Name;
            if (await _typeService.UpdateTypeOfDoor(existTypeOfDoor) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return Ok(_mapper.Map<TypeOfDoorResponse>(existTypeOfDoor));
        }

        [HttpDelete(ApiRoutes.TypeOfDoors.DeleteType)]
        public async Task<IActionResult> DeleteType(int typeId)
        {
            var typeExists = await _typeService.GetTypeOfDoor(typeId);
            if (typeExists is null)
            {
                return NotFound();
            }

            if (await _typeService.DeleteTypeOfDoor(typeExists) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }
            return NoContent();
        }

       
    }
}