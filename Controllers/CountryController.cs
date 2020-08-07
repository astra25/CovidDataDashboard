using CovidDataDashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CovidDataDashboard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryManager _countryManager;

        public CountryController(ICountryManager countryManager)
        {
            _countryManager = countryManager;
        }

        [HttpGet]
        public IActionResult Get(string code)
        {
            var country = _countryManager.GetCountry(code);

            if (country == null)
            {
                return NotFound();
            }

            return new JsonResult(country);
        }

        //[HttpGet]
        //public IActionResult Get(string[] code)
        //{
        //    var countries = _countryManager.GetCountries(code);

        //    if (countries == null)
        //    {
        //        return NotFound();
        //    }

        //    return new JsonResult(countries);
        //}
    }
}