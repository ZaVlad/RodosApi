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
    public class MaterialController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IMaterialService _materialService;
        public MaterialController( IUriService uriService, IMapper mapper, IMaterialService materialService)
        {
            _uriService = uriService;
            _mapper = mapper;
            _materialService = materialService;
        }

        [HttpGet(ApiRoutes.Material.GetMaterials)]
        public async Task<IActionResult> GetMaterials(PaginationQuery paginationQuery,MaterialSortingQuery materialSorting,string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<MaterialSorting>(materialSorting);
            var materials = await _materialService.GetAllMaterial(pagination,sorting,name);
            var materialResponse = _mapper.Map<List<MaterialResponse>>(materials);

            if (pagination is null || pagination.PageSize < 1 || pagination.PageNumber < 1)
            {
                return Ok(materialResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse<MaterialResponse>(_uriService, pagination, materialResponse);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Material.GetMaterial)]
        public async Task<IActionResult> GetMaterial(long materialId)
        {
            var material = await _materialService.GetMaterial(materialId);
            if (material is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MaterialResponse>(material));
        }

        [HttpPost(ApiRoutes.Material.CreateMaterial)]
        public async Task<IActionResult> CreateMaterial([FromBody]MaterialToCreate materialToCreate)
        {
            bool materialValidation = await _materialService.ValidationMaterial(materialToCreate.Name);
            if (!materialValidation)
            {
                ModelState.AddModelError("", "Material with the same name is already exists");
                return Conflict(ModelState);
            }

            var material = _mapper.Map<Material>(materialToCreate);
            if (await _materialService.CreateMaterial(material) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }
            string baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Material.GetMaterial)
                .Replace("{materialId}", material.MaterialId.ToString());
            return Created(new Uri(baseUri), material);
        }

        [HttpPut(ApiRoutes.Material.UpdateMaterial)]
        public async Task<IActionResult> UpdateMaterial(long materialId, [FromBody] MaterialToUpdate materialToUpdate)
        {
            var material = await _materialService.GetMaterial(materialId);
            if (material is null)
            {
                return NotFound();
            }

            var validationMaterial = await _materialService.ValidationMaterial(materialToUpdate.Name, materialId);
            if (validationMaterial == false)
            {
                ModelState.AddModelError("", "Material with the same name is already exists");
                return Conflict(ModelState);
            }

            material.Name = materialToUpdate.Name;
            await _materialService.UpdateMaterial(material);
            return Ok(material);
        }

        [HttpDelete(ApiRoutes.Material.DeleteMaterial)]
        public async Task<IActionResult> DeleteMaterial(long materialId)
        {
            var material = await _materialService.GetMaterial(materialId);
            if (material is null)
            {
                return NotFound();
            }

            await _materialService.DeleteMaterial(material);
            return NoContent();
        }
        
    }
}