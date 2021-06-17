using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class TypeOfHingesController : Controller
    {
        private readonly ITypeOfHingesService _typeOfHinges;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public TypeOfHingesController(IMapper mapper, IUriService uriService, ITypeOfHingesService typeOfHinges)
        {
            _mapper = mapper;
            _uriService = uriService;
            _typeOfHinges = typeOfHinges;
        }

        [HttpGet(ApiRoutes.TypeOfHinges.GetTypesOfHinges)]
        public async Task<IActionResult> GetAllTypesOfHinges(PaginationQuery paginationQuery,TypeOfHingesSortingQuery typeOfHingesSorting,string name)
        {

            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<TypeOfHingesSorting>(typeOfHingesSorting);

            var typeOfHinges = await _typeOfHinges.GetAllTypesOfHinges(pagination,sorting,name);
            var typeOfHingesResponse = _mapper.Map<List<TypeOfHingeResponse>>(typeOfHinges);

            if (pagination is null || pagination.PageSize < 1 || pagination.PageNumber < 1)
            {
                return Ok(typeOfHingesResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse<TypeOfHingeResponse>(_uriService, pagination, typeOfHingesResponse);

            return Ok(result);
        }

        [HttpGet(ApiRoutes.TypeOfHinges.GetTypeOfHinges)]
        public async Task<IActionResult> GetTypeOfHinges(long typeOfHingesId)
        {
            var typeOfHinges = await _typeOfHinges.GetTypeOfHinges(typeOfHingesId);
            if (typeOfHinges is null)
            {
                return NotFound();
            }

            var typeOfHingesResponse = _mapper.Map<TypeOfHingeResponse>(typeOfHinges);
            return Ok(typeOfHingesResponse);
        }

        [HttpPost(ApiRoutes.TypeOfHinges.CreateTypeOfHinges)]
        public async Task<IActionResult> CreateTypeOfHinges([FromBody] TypeOfHingesToCreate typeOfHingesToCreate)
        {
            bool validationTypeOfHinges = await _typeOfHinges.TypeOfHingesValidation(typeOfHingesToCreate.Name);
            if (!validationTypeOfHinges)
            {
                ModelState.AddModelError("", "Type of Hinges with the same name is already exists");
                return Conflict(ModelState);
            }

            var typeOfHinges = _mapper.Map<TypeOfHinge>(typeOfHingesToCreate);
            if (await _typeOfHinges.CreateTypeOfHinges(typeOfHinges) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            string baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Coatings.GetCoating)
                .Replace("{typeOfHingesId}", typeOfHinges.TypeOfHingeId.ToString());
            return Created(baseUri, typeOfHinges);
        }

        [HttpPut(ApiRoutes.TypeOfHinges.UpdateTypeOfHinges)]
        public async Task<IActionResult> UpdateTypeOfHinges(long typeOfHingesId, [FromBody] TypeOfHingesToUpdate typeOfHingesToUpdate)
        {
            var typeOfHinges = await _typeOfHinges.GetTypeOfHinges(typeOfHingesId);

            if (typeOfHinges is null)
            {
                return NotFound();
            }

            var validationTypeOfHinges = await _typeOfHinges.TypeOfHingesValidation(typeOfHingesToUpdate.Name, typeOfHingesId);
            if (validationTypeOfHinges == false)
            {
                ModelState.AddModelError("", "Type of Hinges with the same name is already exists");
                return Conflict(ModelState);
            }

            typeOfHinges.Name = typeOfHingesToUpdate.Name;

            if (await _typeOfHinges.UpdateTypeOfHinges(typeOfHinges) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }

            return Ok(_mapper.Map<TypeOfHinge>(typeOfHinges));
        }

        [HttpDelete(ApiRoutes.TypeOfHinges.DeleteTypeOfHinges)]
        public async Task<IActionResult> DeleteTypeOfHinges(long typeOfHingesId)
        {
            var typeOfHinges = await _typeOfHinges.GetTypeOfHinges(typeOfHingesId);
            if (typeOfHinges is null)
            {
                return NotFound();
            }

            if (await _typeOfHinges.DeleteTypeOfHinges(typeOfHinges) == false)
            {
                ModelState.AddModelError("","Something went wrong");
                return BadRequest(ModelState);
            }

            return NoContent();
        }

    }
}
