using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    public class CategoryController : Controller
    {
        private readonly IUriService _uriService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, ICategoryService categoryService, IUriService uriService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Category.GetCategories)]
        public async Task<IActionResult> GetCategories(PaginationQuery paginationQuery,CategorySortingQuery sortingQuery,string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<CategorySorting>(sortingQuery);
            var categories = await _categoryService.GetAllCategory(name,pagination,sorting);
            var categoriesResponse = _mapper.Map<List<CategoryResponse>>(categories);
            if (pagination is null && pagination.PageSize < 1 && pagination.PageNumber < 1)
            {
                return Ok(categoriesResponse);
            }

           var result = PaginationHelpers.CreatePaginationResponse<CategoryResponse>(_uriService, pagination, categoriesResponse);
           return Ok(result);
        }

        [HttpGet(ApiRoutes.Category.GetCategory)]
        public async Task<IActionResult> GetCategory(long categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);
            if (category is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryResponse>(category));
        }

        [HttpPost(ApiRoutes.Category.CreateCategory)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryToCreate categoryToCreate)
        {
            var validationCategory = await _categoryService.ValidationCategory(categoryToCreate.Name);
            if (validationCategory == false)
            {
                ModelState.AddModelError("","Category with the same name is already exists");
                return Conflict(ModelState);
            }

            var category = _mapper.Map<Category>(categoryToCreate);

            if (await _categoryService.CreateCategory(category) == false)
            {
                ModelState.AddModelError("","Something went wrong");
                return BadRequest(ModelState);
            }

            var baseUri = string.Concat(_uriService.BaseUri(),ApiRoutes.Category.GetCategory)
                .Replace("{categoryId}", category.CategoryId.ToString());
            return Created(new Uri(baseUri), category);
        }

        [HttpPut(ApiRoutes.Category.UpdateCategory)]
        public async Task<IActionResult> UpdateCategory(long categoryId, [FromBody] CategoryToUpdate categoryToUpdate)
        {
            var category = await _categoryService.GetCategory(categoryId);
            if (category is null)
            {
                return NotFound();
            }

            var categoryValidation = await _categoryService.ValidationCategory(categoryToUpdate.Name, categoryId);
            if (categoryValidation == false)
            {
                ModelState.AddModelError("","Category with same name is already exists");
                return Conflict(ModelState);
            }

            category.Name = categoryToUpdate.Name;

            if (await _categoryService.UpdateCategory(category) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }

            return Ok(_mapper.Map<CategoryResponse>(category));
        }

        [HttpDelete(ApiRoutes.Category.DeleteCategory)]
        public async Task<IActionResult> DeleteCategory(long categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);

            if (category is null)
            {
                return NotFound();
            }

            if (await _categoryService.DeleteCategory(category) == false)
            {
                ModelState.AddModelError("","Something went wrong");
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}