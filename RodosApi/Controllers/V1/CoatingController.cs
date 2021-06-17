using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class CoatingController : Controller
    {
        private readonly ICoatingService _coatingService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public CoatingController(ICoatingService coatingService, IMapper mapper, IUriService uriService)
        {
            _coatingService = coatingService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Coatings.GetCoatings)]
        public async Task<IActionResult> GetCoatings([FromQuery]PaginationQuery paginationQuery,CoatingSortingQuery sortingQuery,string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<CoatingSorting>(sortingQuery);
            var coating = await _coatingService.GetAllCoatings(pagination,sorting,name);
            var coatingResponse = _mapper.Map<List<CoatingResponse>>(coating);

            if (pagination is null || pagination.PageSize < 1 || pagination.PageNumber < 1)
            {
                return Ok(coatingResponse);
            }

           var result = PaginationHelpers.CreatePaginationResponse<CoatingResponse>(_uriService, pagination, coatingResponse);

           return Ok(result);
        }

        [HttpGet(ApiRoutes.Coatings.GetCoating)]
        public async Task<IActionResult> GetCoating(long coatingId)
        {
            var coating = await _coatingService.GetCoating(coatingId);
            if (coating is null)
            {
                return NotFound();
            }

            var coatingResponse = _mapper.Map<CoatingResponse>(coating);
            return Ok(coatingResponse);
        }

        [HttpPost(ApiRoutes.Coatings.CreateCoating)]
        public async Task<IActionResult> CreateCoating([FromBody] CoatingToCreate coatingToCreate)
        {
            bool validationCoating = await _coatingService.ValidationCoating(coatingToCreate.Name);
            if (!validationCoating)
            {
                ModelState.AddModelError("","Coating with the same name is already exists");
                return Conflict(ModelState);
            }

            var coating = _mapper.Map<Coating>(coatingToCreate);
            if (await _coatingService.CreateCoating(coating) == false)
            {
                ModelState.AddModelError("","Something went wrong");
                return BadRequest();
            }

            string baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Coatings.GetCoating)
                .Replace("{coatingId}", coating.Id.ToString());
            return Created(baseUri, coating);
        }

        [HttpPut(ApiRoutes.Coatings.UpdateCoating)]
        public async Task<IActionResult> UpdateCoating(long coatingId, [FromBody] CoatingToUpdate coatingToUpdate)
        {
            var coating = await _coatingService.GetCoating(coatingId);

            if (coating is null)
            {
                return NotFound();
            }

            var validationCoating = await _coatingService.ValidationCoating(coatingToUpdate.Name, coatingId);
            if (validationCoating == false)
            {
                ModelState.AddModelError("", "Coating with the same name is already exists");
                return Conflict(ModelState);
            }

            coating.Name = coatingToUpdate.Name;

            if (await _coatingService.UpdateCoating(coating) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return Ok(_mapper.Map<CoatingResponse>(coating));
        }

        [HttpDelete(ApiRoutes.Coatings.DeleteCoating)]
        public async Task<IActionResult> DeleteCoating(long coatingId)
        {
            var coating =await _coatingService.GetCoating(coatingId);
            if (coating is null)
            {
                return NotFound();
            }

            if (await _coatingService.DeleteCoating(coating) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return NoContent();
        }
    }
}
