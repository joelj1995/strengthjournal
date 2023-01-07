using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.IAM.API.Models;
using StrengthJournal.IAM.API.Services;

namespace StrengthJournal.IAM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        readonly CountryService countryService;

        public CountriesController(CountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<CountryResponse>> GetCountries()
        {
            return await countryService.GetCountries();
        }
    }
}
