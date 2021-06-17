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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Controllers.V1
{
    public class DoorHandleController : Controller
    {
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        private readonly IDoorHandleService _doorHandle;

        public DoorHandleController(IMapper mapper, IUriService uriService, IDoorHandleService doorHandle)
        {
            _mapper = mapper;
            _uriService = uriService;
            _doorHandle = doorHandle;
        }

        [HttpGet(ApiRoutes.DoorHandle.GetDoorHandles)]
        public async Task<IActionResult> GetDoorHandles(PaginationQuery paginationQuery,DoorHandleQuery doorHandleQuery,DoorHandlesSortingQuery doorHandlesSorting)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var doorHandleFilter = _mapper.Map<DoorHandleFilter>(doorHandleQuery);
            var sorting = _mapper.Map<DoorHandlesSorting>(doorHandlesSorting);
            var doorHandles = await _doorHandle.GetAllDoorHandles(doorHandleFilter, pagination,sorting);
            var doorHandlesResponse = _mapper.Map<List<DoorHandleResponse>>(doorHandles);

            if(pagination is null || pagination.PageNumber<1|| pagination.PageSize < 1)
            {
                return Ok(doorHandlesResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse(_uriService, pagination, doorHandlesResponse);

            return Ok(result);
        }

        [HttpGet(ApiRoutes.DoorHandle.GetDoorHandle)]
        public async Task<IActionResult> GetDoorHandle(long doorHandleId)
        {
            var doorHandle =await  _doorHandle.GetDoorHandle(doorHandleId);
            if(doorHandle is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DoorHandleResponse>(doorHandle));
        }
        [HttpPost(ApiRoutes.DoorHandle.CreateDoorHandle)]
        public async Task<IActionResult> CreateDoorHandle([FromBody] DoorHandleToCreate doorHandleToCreate)
        {
            var validationDoorhandle = await _doorHandle.ValidationdoorHandle(doorHandleToCreate.Name);
            if (validationDoorhandle == false)
            {
                ModelState.AddModelError("", "Door handle with the same name is already exists");
                return Conflict(ModelState);
            }
            var doorHandle = _mapper.Map<DoorHandle>(doorHandleToCreate);
            if (await _doorHandle.CreateDoorHandle(doorHandle) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            var baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.DoorHandle.GetDoorHandle).Replace("{doorHandleId}", doorHandle.DoorHandleId.ToString());
            return Created(baseUri, _mapper.Map<DoorHandleResponse>(doorHandle));
        }
        [HttpPut(ApiRoutes.DoorHandle.UpdateDoorHandle)]
        public async Task<IActionResult> UpdateDoorHandle(long doorHandleId, [FromBody] DoorHandleToUpdate doorHandleToUpdate)
        {
            var doorHandle = await _doorHandle.GetDoorHandle(doorHandleId);
            if (doorHandle is null)
            {
                return NotFound();
            }

            var validationDoorHandle = await _doorHandle.ValidationdoorHandle(doorHandleToUpdate.Name, doorHandleId);

            if (validationDoorHandle == false)
            {
                ModelState.AddModelError("", "Door handle with the same name is already exists");
                return Conflict(ModelState);
            }
            doorHandle = MapDoorHandle(doorHandle, doorHandleToUpdate);

            if (await _doorHandle.UpdateDoorHandle(doorHandle) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }

            return Ok(_mapper.Map<DoorHandleResponse>(doorHandle));
        }
        [HttpDelete(ApiRoutes.DoorHandle.DeleteDoorHandle)]
        public async Task<IActionResult> DeleteDoorHandle(long doorHandleId)
        {
            var doorHandle = await _doorHandle.GetDoorHandle(doorHandleId);
            if (doorHandle is null)
            {
                return NotFound();
            }

            if (await _doorHandle.DeletedoorHandle(doorHandle) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            return NoContent();
        }
        private static DoorHandle MapDoorHandle(DoorHandle doorHandle, DoorHandleToUpdate doorHandleToUpdate)
        {
            doorHandle.Name = doorHandleToUpdate.Name;
            doorHandle.CategoryId = doorHandleToUpdate.CategoryId;
            doorHandle.ColorId = doorHandleToUpdate.ColorId;
            doorHandle.FurnitureTypeId = doorHandleToUpdate.FurnitureTypeId;
            doorHandle.MakerId = doorHandleToUpdate.MakerId;
            doorHandle.MaterialId = doorHandleToUpdate.MaterialId;
            doorHandle.Price = doorHandleToUpdate.Price;
            return doorHandle;
        }
    }
}
