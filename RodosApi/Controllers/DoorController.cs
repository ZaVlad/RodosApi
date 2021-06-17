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

namespace RodosApi.Controllers
{
    public class DoorController : Controller
    {
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        private readonly IDoorService _doorService;

        public DoorController(IMapper mapper, IUriService uriService, IDoorService doorService)
        {
            _mapper = mapper;
            _uriService = uriService;
            _doorService = doorService;
        }
        [HttpGet(ApiRoutes.Door.GetDoors)]
        public async Task<IActionResult> GetDoors(PaginationQuery paginationQuery, DoorQuery doorQuery,DoorSortingQuery doorSorting)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var doorFilter = _mapper.Map<DoorFilter>(doorQuery);
            var sorting = _mapper.Map<DoorSorting>(doorSorting);
            var doors = await _doorService.GetDoors(doorFilter, pagination,sorting);
           
            var doorResponse = _mapper.Map<List<DoorResponse>>(doors);


            if (pagination is null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(doorResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse(_uriService, pagination, doorResponse);
            return Ok(result);
        }

       

        [HttpGet(ApiRoutes.Door.GetDoor)]
        public async Task<IActionResult> GetDoor(long doorId)
        {
            var door = await _doorService.GetDoor(doorId);
            if (door is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DoorResponse>(door));
        }
        [HttpPost(ApiRoutes.Door.CreateDoor)]
        public async Task<IActionResult> CreateDoor([FromBody] DoorToCreate doorToCreate)
        {
            var validationDoor = await _doorService.ValidationDoor(doorToCreate.Name);
            if (validationDoor == false)
            {
                ModelState.AddModelError("", "Door with the same name is already exists");
                return Conflict(ModelState);
            }
            var door = _mapper.Map<Door>(doorToCreate);
            if (await _doorService.CreateDoor(door) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            var baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Door.GetDoor).Replace("{doorId}", door.DoorId.ToString());
            return Created(baseUri, $"{door.DoorId} {door.Name}");
        }
        //[HttpPost(ApiRoutes.Door.CreateDoors)]
        //public async Task<IActionResult> CreateDoors([FromBody] List<DoorToCreate> doorsToCreate)
        //{
        //    bool validationDoor;
        //    foreach (var door in doorsToCreate)
        //    {
        //        validationDoor = await _doorService.ValidationDoor(door.Name);
        //        if (validationDoor == false)
        //        {
        //            ModelState.AddModelError("", $"Door with {door.Name} is already exists");
        //            return Conflict(ModelState);
        //        }

        //    }
        //    var doors = _mapper.Map<List<Door>>(doorsToCreate);
        //    if (await _doorService.CreateDoors(doors) == false)
        //    {
        //        ModelState.AddModelError("", "Something went wrong");
        //        return BadRequest(ModelState);
        //    }

        //    var baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Door.GetDoors);
        //    return Created(new Uri(baseUri), "");
        //}
        [HttpPut(ApiRoutes.Door.UpdateDoor)]
        public async Task<IActionResult> UpdateDoor(long doorId, [FromBody] DoorToUpdate doorToUpdate)
        {
            var door = await _doorService.GetDoor(doorId);
            if (door is null)
            {
                return NotFound();
            }

            var validationDoor = await _doorService.ValidationDoor(doorToUpdate.Name, doorId);

            if (validationDoor == false)
            {
                ModelState.AddModelError("", "Door with the same name is already exists");
                return Conflict(ModelState);
            }
            door = MapDoor(door, doorToUpdate);

            if (await _doorService.UpdateDoor(door) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }

            return Ok(_mapper.Map<DoorResponse>(door));
        }

        [HttpDelete(ApiRoutes.Door.DeleteDoor)]
        public async Task<IActionResult> DeleteDoor(long doorId)
        {
            var door = await _doorService.GetDoor(doorId);
            if (door is null)
            {
                return NotFound();
            }
            if (await _doorService.DeleteDoor(door) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            return NoContent();
        }
        private static Door MapDoor(Door door, DoorToUpdate doorToUpdate)
        {
            door.CategoryId = doorToUpdate.CategoryId;
            door.CoatingId = doorToUpdate.CoatingId;
            door.CollectionId = doorToUpdate.CollectionId;
            door.ColorId = doorToUpdate.ColorId;
            door.Description = doorToUpdate.Description;
            door.DoorHandleId = doorToUpdate.DoorHandleId;
            door.DoorModelId = doorToUpdate.DoorModelId;
            door.HingesId = doorToUpdate.HingesId;
            door.MakerId = doorToUpdate.MakerId;
            door.Name = doorToUpdate.Name;
            door.Price = doorToUpdate.Price;

            return door;
        }

       
    }
}
