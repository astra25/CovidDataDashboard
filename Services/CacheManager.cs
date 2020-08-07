using CovidDataDashboard.Interfaces;
using CovidDataDashboard.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CovidDataDashboard.Services
{
    public class CacheManager : ICacheManager
    {
        private readonly MemoryCache _cache;

        public CacheManager()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            InitializeCache();
        }

        public TItem GetCacheItem<TItem>(object key)
        {
            TItem cacheItem;

            var success = _cache.TryGetValue(key, out cacheItem);

            return success ? cacheItem : throw new KeyNotFoundException();
        }

        private void InitializeCache()
        {
            var json = File.ReadAllText("data/owid-covid-data.json");

            var contractResolver = new DefaultContractResolver
            { 
                NamingStrategy = new SnakeCaseNamingStrategy() 
            };

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            };

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, Country>>(json, jsonSettings);

            var continents = new List<Continent>();

            foreach (var item in dictionary)
            {
                var countryData = item.Value.Data.ToList();

                for (int i = 0; i < countryData.Count; i++)
                {
                    var countryItem = countryData[i];
                    countryItem.NewCasesAverage = countryData.Skip(i - 6).Take(7).Average(x => x.NewCases);
                }

                _cache.Set(item.Key, item.Value);

                var continentName = item.Value.Continent;

                if (continentName == null)
                {
                    continue;
                }

                var continent = continents.FirstOrDefault(x => x.Name == continentName);

                var countryKeyValue = new CountryKeyValue
                {
                    CountryKey = item.Key,
                    CountryValue = item.Value.Location
                };

                if (continent == null)
                {
                    continent = new Continent()
                    {
                        Name = continentName,
                        Countries = new List<CountryKeyValue>()
                        {
                            countryKeyValue
                        }
                    };

                    continents.Add(continent);
                }
                else
                {
                    continent.Countries.Add(countryKeyValue);
                }
            }

            foreach (var continent in continents)
            {
                _cache.Set(continent.Name, continent);
            }
        }
    }
}
