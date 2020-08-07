using System;
using System.Collections.Generic;

namespace CovidDataDashboard.Models
{
    public class Country
    {
        public string Continent { get; set; }
        public string Location { get; set; }
        public double Population { get; set; }
        public double PopulationDensity { get; set; }
        public IEnumerable<CovidData> Data { get; set; }
    }

    public class CovidData
    {
        public DateTime Date { get; set; }
        public double TotalCases { get; set; }
        public double NewCases { get; set; }
        public double NewCasesAverage { get; set; }
        public double TotalDeaths { get; set; }
        public double NewDeaths { get; set; }
        public double TotalCasesPerMillion { get; set; }
        public double NewCasesPerMillion { get; set; }
        public double TotalDeathsPerMillion { get; set; }
        public double NewDeathsPerMillion { get; set; }
    }
}
