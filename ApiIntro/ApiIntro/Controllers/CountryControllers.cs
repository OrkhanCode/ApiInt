
using ApiIntro.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Metrics;

namespace ApiIntro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryControllers : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetAll()
        {
            return Ok(await _countryService.GetAllCountries());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetById(int id)
        {
            var country = await _countryService.GetCountryById(id);

            if (country == null) return NotFound();

            return Ok(country);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<IEnumerable<Country>>> GetBySearchText(string searchText)
        {
            var countries = await _countryService.GetCountriesBySearchText(searchText);
            return Ok(countries);
        }

        [HttpPost]
        public async Task<ActionResult<Country>> Create([FromBody] CountryCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid model state", details = ModelState });
            }

            var country = await _countryService.CreateCountry(request);

            return CreatedAtAction(nameof(GetById), new { id = country.Id }, country);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CountryEditDto request)
        {
            var updatedCountry = await _countryService.UpdateCountry(id, request);

            if (updatedCountry == null) return NotFound();

            return Ok(updatedCountry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _countryService.DeleteCountry(id);

            if (!result) return NotFound();

            return Ok();
        }
    }

}
}
