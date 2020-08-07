using CovidDataDashboard.Models;
using System.Collections.Generic;

namespace CovidDataDashboard.Interfaces
{
    public interface ICountryManager
    {
        public Country GetCountry(string code);
        public IEnumerable<Country> GetCountries(string[] codes);
        public IEnumerable<Continent> GetContinents(string[] names);
    }
}
