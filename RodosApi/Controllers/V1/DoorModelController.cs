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
    public class DoorModelController : Controller
    {
        private readonly IDoorModelService _doorModelService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public DoorModelController(IDoorModelService doorModelService, IMapper mapper, IUriService uriService)
        {
            _doorModelService = doorModelService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.DoorModel.GetDoorModels)]
        public async Task<IActionResult> GetDoorModels([FromQuery] PaginationQuery paginationQuery,DoorModelSortingQuery doorModelSortingQuery,string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<DoorModelSorting>(doorModelSortingQuery);
            var doorModels =await _doorModelService.GetAllDoorModels(pagination,sorting,name);
            var doorModelResponse = _mapper.Map<List<DoorModelResponse>>(doorModels);

            if (pagination is null || pagination.PageSize < 1 || pagination.PageNumber < 1)
            {
                return Ok(doorModelResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse<DoorModelResponse>(_uriService, pagination, doorModelResponse);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.DoorModel.GetDoorModel)]
        public async Task<IActionResult> GetDoorModel(long doorModelId)
        {
            var doorModel = await _doorModelService.GetDoorModel(doorModelId);

            if (doorModel is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DoorModelResponse>(doorModel));
        }

        [HttpPost(ApiRoutes.DoorModel.CreateDoorModel)]
        public async Task<IActionResult> CreateCoating([FromBody] DoorModelToCreate doorModelToCreate)
        {
            bool validationDoorModel = await _doorModelService.ValidateDoorModel(doorModelToCreate.Name);
            if (!validationDoorModel)
            {
                ModelState.AddModelError("", "Door Model with the same name is already exists");
                return Conflict(ModelState);
            }

            var doorModel = _mapper.Map<DoorModel>(doorModelToCreate);
            if (await _doorModelService.CreateDoorModel(doorModel) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            string baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.DoorModel.GetDoorModel)
                .Replace("{doorModelId}", doorModel.Id.ToString());
            return Created(baseUri, doorModel);
        }

        [HttpPut(ApiRoutes.DoorModel.UpdateDoorModel)]
        public async Task<IActionResult> UpdateCoating(long doorModelId, [FromBody] DoorModelToUpdate doorModelToUpdate)
        {
            var doorModel = await _doorModelService.GetDoorModel(doorModelId);

            if (doorModel is null)
            {
                return NotFound();
            }

            var validationDoorModel = await _doorModelService.ValidateDoorModel(doorModelToUpdate.Name, doorModelId);
            if (validationDoorModel == false)
            {
                ModelState.AddModelError("", "Door Model with the same name is already exists");
                return Conflict(ModelState);
            }

            doorModel.Name = doorModelToUpdate.Name;

            if (await _doorModelService.UpdateDoorModel(doorModel) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return Ok(_mapper.Map<DoorModelResponse>(doorModel));
        }

        [HttpDelete(ApiRoutes.DoorModel.DeleteDoorModel)]
        public async Task<IActionResult> DeleteCoating(long doorModelId)
        {
            var doorModel = await _doorModelService.GetDoorModel(doorModelId);
            if (doorModel is null)
            {
                return NotFound();
            }

            if (await _doorModelService.DeleteDoorModel(doorModel) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return NoContent();
        }
    }
}
