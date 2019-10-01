using Pyrox.BlazorComponents.DataGrid.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pyrox.BlazorComponents.DataGrid.E2ETests.Data
{
    public class WeatherForecastService : IDataGridService<WeatherForecast, WeatherForecastSortKey>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly WeatherForecast[] Data =
            Enumerable.Range(1, 145).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = new Random().Next(-20, 55),
                Summary = Summaries[new Random().Next(Summaries.Length)]
            }).ToArray();

        public async Task<List<WeatherForecast>> GetItemsAsync(
            int pageNumber,
            int pageSize,
            SortInformation<WeatherForecastSortKey> sortInfo = null,
            string searchQuery = null)
        {
            var query = Data.AsQueryable();

            if (!(sortInfo is null))
            {
                if (sortInfo.Type == SortType.Ascending)
                {
                    switch (sortInfo.Key)
                    {
                        case WeatherForecastSortKey.Date:
                        default:
                            query = query.OrderBy(f => f.Date);
                            break;
                        case WeatherForecastSortKey.TemperatureC:
                            query = query.OrderBy(f => f.TemperatureC);
                            break;
                        case WeatherForecastSortKey.TemperatureF:
                            query = query.OrderBy(f => f.TemperatureF);
                            break;
                        case WeatherForecastSortKey.Summary:
                            query = query.OrderBy(f => f.Summary);
                            break;
                    }
                }
                else
                {
                    switch (sortInfo.Key)
                    {
                        case WeatherForecastSortKey.Date:
                        default:
                            query = query.OrderByDescending(f => f.Date);
                            break;
                        case WeatherForecastSortKey.TemperatureC:
                            query = query.OrderByDescending(f => f.TemperatureC);
                            break;
                        case WeatherForecastSortKey.TemperatureF:
                            query = query.OrderByDescending(f => f.TemperatureF);
                            break;
                        case WeatherForecastSortKey.Summary:
                            query = query.OrderByDescending(f => f.Summary);
                            break;
                    }
                }
            }

            if (!(searchQuery is null))
            {
                var searchQueryLowercase = searchQuery.ToLower();
                query = query.Where(f => f.Date.ToString().ToLower().Contains(searchQueryLowercase)
                    || f.TemperatureC.ToString().Contains(searchQuery)
                    || f.TemperatureF.ToString().Contains(searchQuery)
                    || f.Summary.ToLower().Contains(searchQueryLowercase));
            }

            var items = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return items;
        }

        public async Task<int> GetItemCountAsync(string searchQuery = null)
        {
            if (!(searchQuery is null))
            {
                var searchQueryLowercase = searchQuery.ToLower();
                return Data.Where(f => f.Date.ToString().ToLower().Contains(searchQueryLowercase)
                    || f.TemperatureC.ToString().Contains(searchQuery)
                    || f.TemperatureF.ToString().Contains(searchQuery)
                    || f.Summary.ToLower().Contains(searchQueryLowercase))
                    .Count();
            }

            return Data.Length;
        }
    }
}
