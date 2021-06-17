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
    public class MakerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IMakerService _makerService;

        public MakerController(IMapper mapper, IUriService uriService, IMakerService makerService)
        {
            _mapper = mapper;
            _uriService = uriService;
            _makerService = makerService;
        }
        [HttpGet(ApiRoutes.Maker.GetMakers)]
        public async Task<IActionResult> GetMakers(PaginationQuery paginationQuery,MakerSortingQuery makerSortingQuery,MakerFilterQuery makerFilterQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var makerFilter = _mapper.Map<MakerFilter>(makerFilterQuery);
            var makerSorting = _mapper.Map<MakerSorting>(makerSortingQuery);

            var makers =await _makerService.GetAllMakers(pagination,makerSorting,makerFilter);
            var makersResponse = _mapper.Map<List<MakerResponse>>(makers);
            if (pagination is null || pagination.PageSize >1 || pagination.PageNumber > 1)
            {
                return Ok(makersResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse<MakerResponse>(_uriService, pagination, makersResponse);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Maker.GetMaker)]
        public async Task<IActionResult> GetMaker(long makerId)
        {
            var maker = await  _makerService.GetMaker(makerId);
            if (maker is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MakerResponse>(maker));
        }

        [HttpPost(ApiRoutes.Maker.CreateMaker)]
        public async Task<IActionResult> CreateMaker([FromBody] MakerToCreate makerToCreate)
        {
            var makerValidation = await _makerService.ValidationMaker(makerToCreate.Name);
            if (makerValidation == false)
            {
                ModelState.AddModelError("","Maker with the same name is already exists");
                return Conflict(ModelState);
            }

            var maker = _mapper.Map<Maker>(makerToCreate);
            if (await _makerService.CreateMaker(maker) == false)
            {
                ModelState.AddModelError("","Something went wrong");
                return BadRequest(ModelState);
            }

            string baseUri = string.Concat(_uriService.BaseUri(), "/", ApiRoutes.Maker.GetMaker)
                .Replace("{makerId}", maker.MakerId.ToString());
            return Created(new Uri(baseUri),_mapper.Map<MakerResponse>(maker));
        }

        [HttpPut(ApiRoutes.Maker.UpdateMaker)]

        public async Task<IActionResult> UpdateMaker(long makerId,[FromBody] MakerToUpdate makerToUpdate)
        {
            var maker = await _makerService.GetMaker(makerId);
            if (maker is null)
            {
                return NotFound();
            }
            var makerValidation = await _makerService.ValidationMaker(makerToUpdate.Name,makerId);
            if (makerValidation == false)
            {
                ModelState.AddModelError("", "Maker with the same name is already exists");
                return Conflict(ModelState);
            }

            maker.Name = makerToUpdate.Name;
            maker.CountryId = makerToUpdate.CountryId;

            if (await _makerService.UpdateMaker(maker) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }

            return Ok(_mapper.Map<MakerResponse>(maker));
        }
        [HttpDelete(ApiRoutes.Maker.DeleteMaker)]
        public async Task<IActionResult> DeleteMaker(long makerId)
        {
            var maker = await _makerService.GetMaker(makerId);
            if (maker is null)
            {
                return NotFound();
            }

            if (await _makerService.DeleteMaker(maker) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
