using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RodosApi.Services;
using RodosApi.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RodosApi.Domain;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Domain.SortingDomain;
using RodosApi.Contract.V1.Response;
using RodosApi.Helpers;
using RodosApi.Contract.V1.Request;

namespace RodosApi.Controllers.V1
{
    public class HingesController : Controller
    {
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        private readonly IHingesService _hingesService;

        public HingesController(IUriService uriService, IMapper mapper, IHingesService hingesService = null)
        {
            _uriService = uriService;
            _mapper = mapper;
            _hingesService = hingesService;
        }

        [HttpGet(ApiRoutes.Hinges.GetHinges)]
        public async Task<IActionResult> GetHinges( GetAllHingesQuery query,PaginationQuery paginationQuery,HingesSortingQuery hingesSortingQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<HingesSorting>(hingesSortingQuery);
            var filter = _mapper.Map<GetAllHingesFilter>(query);
            var hinges = await _hingesService.GetHinges(filter, pagination,sorting);
            var hingesResponse = _mapper.Map<List<HingesResponse>>(hinges);
            if(pagination is null || pagination.PageNumber<1 || pagination.PageSize < 1)
            {
                return Ok(hingesResponse);
            }
            var result = PaginationHelpers.CreatePaginationResponse<HingesResponse>(_uriService, pagination, hingesResponse);

            return Ok(result);
        }
        [HttpGet(ApiRoutes.Hinges.GetHinge)]
        public async Task<IActionResult> GetHinge(long hingesId)
        {
            var hinge = await _hingesService.GetHinge(hingesId);
            if(hinge is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HingesResponse>(hinge));
        }
        [HttpPost(ApiRoutes.Hinges.CreateHinge)]
        public async Task<IActionResult> CreateHinge([FromBody] HingeToCreate hingeToCreate)
        {
            var validationHigne = await _hingesService.ValidationHinges(hingeToCreate.Name);
            if(validationHigne == false)
            {
                ModelState.AddModelError("", "Hinge with the same name is already exists");
                return Conflict(ModelState);
            }
            var hinge = _mapper.Map<Hinges>(hingeToCreate);
            if(await _hingesService.CreateHinges(hinge) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            var baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Hinges.GetHinge).Replace("{hingesId}", hinge.HingesId.ToString());
            return Created(baseUri, _mapper.Map<HingesResponse>(hinge));
        }

        [HttpPut(ApiRoutes.Hinges.UpdateHinge)]
        public async Task<IActionResult> UpdateHinge(long hingesId,[FromBody] HingeToUpdate hingeToUpdate)
        {
            var hinge = await _hingesService.GetHinge(hingesId);
            if(hinge is null)
            {
                return NotFound();
            }

            var validationHinge = await _hingesService.ValidationHinges(hingeToUpdate.Name, hingesId);

            if(validationHinge == false)
            {
                ModelState.AddModelError("", "Hinge with the same name is already exists");
                return Conflict(ModelState);
            }
            hinge = MappHinge(hinge, hingeToUpdate);

            if (await _hingesService.UpdateHinges(hinge) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }

            return Ok(_mapper.Map<HingesResponse>(hinge));
        }
        [HttpDelete(ApiRoutes.Hinges.DeleteHinge)]
        public async Task<IActionResult> DeleteHinge(long hingesId)
        {
            var hinge = await _hingesService.GetHinge(hingesId);
            if(hinge is null)
            {
                return NotFound();
            }

            if(await _hingesService.DeleteHinges(hinge) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        private static Hinges MappHinge(Hinges hinge, HingeToUpdate hingeToUpdate)
        {
            hinge.CategoryId = hingeToUpdate.CategoryId;
            hinge.FurnitureTypeId = hingeToUpdate.FurnitureTypeId;
            hinge.MakerId = hingeToUpdate.MakerId;
            hinge.MaterialId = hingeToUpdate.MaterialId;
            hinge.Name = hingeToUpdate.Name;
            hinge.Price = hingeToUpdate.Price;
            hinge.TypeOfHingesId = hingeToUpdate.TypeOfHingesId;
            return hinge;
        }
    }
}
