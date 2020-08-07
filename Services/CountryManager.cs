using CovidDataDashboard.Interfaces;
using CovidDataDashboard.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CovidDataDashboard.Services
{
    public class CountryManager : ICountryManager
    {
        private readonly ICacheManager _cacheManager;
        private readonly ILogger<CountryManager> _logger;

        public CountryManager(ICacheManager cacheManager, ILogger<CountryManager> logger)
        {
            _cacheManager = cacheManager;
            _logger = logger;
        }

        public Country GetCountry(string code)
        {
            try
            {
                var country = _cacheManager.GetCacheItem<Country>(code);
                return country;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public IEnumerable<Country> GetCountries(string[] codes)
        {
            foreach (var code in codes)
            {
                var country = _cacheManager.GetCacheItem<Country>(code);
                yield return country;
            }
        }

        public IEnumerable<Continent> GetContinents(string[] names)
        {
            var continents = new List<Continent>();

            foreach (var name in names)
            {
                try
                {
                    var continent = _cacheManager.GetCacheItem<Continent>(name);
                    continents.Add(continent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return continents;
        }
    }
}
