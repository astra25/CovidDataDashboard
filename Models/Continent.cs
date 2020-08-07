using System.Collections.Generic;

namespace CovidDataDashboard.Models
{
    public class Continent
    {
        public string Name { get; set; }
        public List<CountryKeyValue> Countries { get; set; }
    }

    public class CountryKeyValue
    {
        public string CountryKey { get; set; }

        public string CountryValue { get; set; }
    }
}
