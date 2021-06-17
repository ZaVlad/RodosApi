using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    
    public class CountryController : Controller
    {
        // GET
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly ICountryService _countryService;

        public CountryController(IMapper mapper, IUriService uriService, ICountryService countryService)
        {
            _mapper = mapper;
            _uriService = uriService;
            _countryService = countryService;
        }

        [HttpGet(ApiRoutes.Countries.GetCountries)]
        public async Task<IActionResult> GetCountries(PaginationQuery paginationQuery,CountrySortingQuery countrySortingQuery,string name)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<CountrySorting>(countrySortingQuery);
            var countries = await _countryService.GetAllCountry(pagination, sorting,name);
            var countryResponse = _mapper.Map<List<CountryResponse>>(countries);

            if (pagination is null || pagination.PageSize < 1 || pagination.PageNumber < 1)
            {
                return Ok(countryResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse<CountryResponse>(_uriService, pagination, countryResponse);
            return Ok(result);
        }
        [HttpGet(ApiRoutes.Countries.GetCountry)]
        public async Task<IActionResult> GetCountry(long countryId)
        {
            var country = await _countryService.GetCountry(countryId);
            if (country is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CountryResponse>(country));
        }

        [HttpPost(ApiRoutes.Countries.CreateCountry)]
        public async Task<IActionResult> CreateCountry([FromBody] CountryToCreate countryToCreate)
        {
            bool validationCountry = await _countryService.ValidationCountry(countryToCreate.Name);
            if (!validationCountry)
            {
                ModelState.AddModelError("", "Country with the same name is already exists");
                return Conflict(ModelState);
            }

            var country = _mapper.Map<Country>(countryToCreate);
            if (await _countryService.CreateCountry(country) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest();
            }
            string baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Countries.GetCountry)
                .Replace("{countryId}", country.CountryId.ToString());
            return Created(new Uri(baseUri), country);
        }
        [HttpPut(ApiRoutes.Countries.UpdateCountry)]
        public async Task<IActionResult> UpdateCountry(long countryId,[FromBody] CountryToUpdate countryToUpdate)
        {
            var country = await _countryService.GetCountry(countryId);
            if (country is null)
            {
                return NotFound();
            }

            var countryValidation = await _countryService.ValidationCountry(countryToUpdate.Name, countryId);
            if (countryValidation == false)
            {
                ModelState.AddModelError("", "Country with the same name is already exists");
                return Conflict(ModelState);
            }

            country.Name = countryToUpdate.Name;
            await _countryService.UpdateCountry(country);
            return Ok(country);
        }
        [HttpDelete(ApiRoutes.Countries.DeleteCountry)]
        public async Task<IActionResult> DeleteCountry(long countryId)
        {
            var country = await _countryService.GetCountry(countryId);
            if (country is null)
            {
                return NotFound();
            }

            await _countryService.DeleteCountry(country);
            return NoContent();
        }
    }
}