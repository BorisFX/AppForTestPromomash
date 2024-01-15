using AppForTest.Application.DTOs;
using AppForTest.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppForTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: /Country
        // Get all countres
        [HttpGet]
        [ProducesResponseType(typeof(CountryDto[]), 200)]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _mediator.Send(new GetAllCountriesQuery());
            return Ok(countries);
        }

        // GET: /Country/{countryId}/Provinces
        // Get provinces by country Id
        [HttpGet("{countryId}/Provinces")]
        [ProducesResponseType(typeof(ProvinceDto[]), 200)]
        public async Task<IActionResult> GetProvincesByCountry(int countryId)
        {
            var provinces = await _mediator.Send(new GetProvincesQuery { CountryId = countryId });
            return Ok(provinces);
        }
    }
}

