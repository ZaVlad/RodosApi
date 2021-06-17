using System;
using System.Collections.Generic;
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
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public ColorController(IColorService colorService, IMapper mapper, IUriService uriService)
        {
            _colorService = colorService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Colors.GetColors)]
        public async Task<IActionResult> GetColors(PaginationQuery paginationQuery,ColorSortingQuery colorSortingQuery,string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<ColorSorting>(colorSortingQuery);
            var colors = await _colorService.GetAllColors(pagination,sorting,name);
            var colorsResponse = _mapper.Map<List<ColorResponse>>(colors);
            if (pagination is null || pagination.PageSize < 1 || pagination.PageNumber < 1)
            {
                return Ok(colorsResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse<ColorResponse>(_uriService, pagination, colorsResponse);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Colors.GetColor)]
        public async Task<IActionResult> GetColor(long colorId)
        {
            var color = await _colorService.GetColor(colorId);
            if (color is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ColorResponse>(color));
        }

        [HttpPost(ApiRoutes.Colors.CreateColor)]
        public async Task<IActionResult> CreateColor([FromBody] ColorToCreate colorToCreate)
        {
            var colorValidation =await _colorService.ValidationColor(colorToCreate.Name);
            if (!colorValidation)
            {
                ModelState.AddModelError("","Color with the same is already exists");
                return Conflict(ModelState);
            }

            var color = _mapper.Map<Color>(colorToCreate);

            if (await _colorService.CreateColor(color) == false)
            {
                ModelState.AddModelError("","Something Went Wrong");
                return BadRequest(ModelState);
            }

            var url = _uriService.BaseUri();
            var baseUri = string.Concat(url, "/", ApiRoutes.Colors.GetColor).Replace("{colorId}", color.Id.ToString());
            return Created(new Uri(baseUri),color);
        }

        [HttpPut(ApiRoutes.Colors.UpdateColor)]
        public async Task<IActionResult> UpdateColor(long colorId, [FromBody] ColorToUpdate colorToUpdate)
        {
            var color = await _colorService.GetColor(colorId);
            if (color is null)
            {
                return NotFound();
            }

            var validationColor = await _colorService.ValidationColor(colorToUpdate.Name, colorId);
            if (!validationColor)
            {
                ModelState.AddModelError("", "Color with the same is already exists");
                return Conflict(ModelState);
            }

            color.Name = colorToUpdate.Name;

            if (await _colorService.UpdateColor(color) == false)
            {
                ModelState.AddModelError("","Something went wrong");
                return BadRequest(ModelState);
            }

            return Ok(color);
        }

        [HttpDelete(ApiRoutes.Colors.DeleteColor)]
        public async Task<IActionResult> DeleteColor(long colorId)
        {
            var color = await _colorService.GetColor(colorId);
            if (color is null)
            {
                return NotFound();
            }

            if (await _colorService.DeleteColor(color) == false)
            {
                ModelState.AddModelError("","Something Went Wrong");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}