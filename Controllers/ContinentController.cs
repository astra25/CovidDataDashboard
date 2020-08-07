using CovidDataDashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CovidDataDashboard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContinentController : ControllerBase
    {
        private readonly ICountryManager _countryManager;

        public ContinentController(ICountryManager countryManager)
        {
            _countryManager = countryManager;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string[] name)
        {
            var continents = _countryManager.GetContinents(name);

            if (continents == null)
            {
                return NotFound();
            }

            return new JsonResult(continents);
        }
    }
}