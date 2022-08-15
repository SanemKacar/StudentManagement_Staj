using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CountryManagement.Services;
using CountryManagement.Models;
using MongoDB.Bson;

namespace CountryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        // GET: api/<CountryController>
        [HttpGet]
        public ActionResult<List<Country>> Get()
        {
            return countryService.Get();
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public ActionResult<Country> Get(string id)
        {
            var country = countryService.Get(id);
            if (country == null)
            {
                return NotFound($"Country with Id = {id} not found");
            }
            return country;
        }

        [HttpGet("{CountryName}/{id}")]
        public ActionResult<List<Country>> Get(string CountryName, string id)
        {
            var country = countryService.Get(CountryName, id);
            if (country == null)
            {
                return NotFound($"Country with Name = {CountryName} not found");
            }
            return country.ToList();
        }
        // POST api/<CountryController>
        [HttpPost]
        public ActionResult<Country> Post([FromBody] Country country)
        {
            countryService.Create(country);
            return CreatedAtAction(nameof(Get), new { id = country.Id }, country);
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Country country)
        {
            var ExistingStudent = countryService.Get(id);
            if (ExistingStudent == null)
            {
                return NotFound($"Country with Id = {id} not found");
            }
            countryService.Update(id, country);
            return NoContent();
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var country = countryService.Get(id);
            if (country == null)
            {
                return NotFound($"Country with Id = {id} not found");
            }
            countryService.Remove(country.Id.ToString());
            return Ok($"Country with Id = {id} deleted");
        }
    }
}
