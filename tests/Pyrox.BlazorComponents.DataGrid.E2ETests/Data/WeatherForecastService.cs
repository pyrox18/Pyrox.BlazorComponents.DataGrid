using Pyrox.BlazorComponents.DataGrid.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pyrox.BlazorComponents.DataGrid.E2ETests.Data
{
    public class WeatherForecastService : IDataGridService<WeatherForecast>
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
            SortInformation<WeatherForecast> sortInfo = null,
            string searchQuery = null,
            object parameters = null)
        {
            var query = Data.AsQueryable();

            if (!(sortInfo is null))
            {
                if (sortInfo.Type == SortType.Ascending)
                {
                    switch (sortInfo.Key)
                    {
                        case nameof(WeatherForecast.Date):
                        default:
                            query = query.OrderBy(f => f.Date);
                            break;
                        case nameof(WeatherForecast.TemperatureC):
                            query = query.OrderBy(f => f.TemperatureC);
                            break;
                        case nameof(WeatherForecast.TemperatureF):
                            query = query.OrderBy(f => f.TemperatureF);
                            break;
                        case nameof(WeatherForecast.Summary):
                            query = query.OrderBy(f => f.Summary);
                            break;
                    }
                }
                else
                {
                    switch (sortInfo.Key)
                    {
                        case nameof(WeatherForecast.Date):
                        default:
                            query = query.OrderByDescending(f => f.Date);
                            break;
                        case nameof(WeatherForecast.TemperatureC):
                            query = query.OrderByDescending(f => f.TemperatureC);
                            break;
                        case nameof(WeatherForecast.TemperatureF):
                            query = query.OrderByDescending(f => f.TemperatureF);
                            break;
                        case nameof(WeatherForecast.Summary):
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

            if (!(parameters is null))
            {
                var type = parameters.GetType();
                if (!(type.GetProperty(nameof(WeatherForecast.Summary)) is null))
                {
                    query = query.Where(f => f.Summary == type.GetProperty(nameof(WeatherForecast.Summary)).GetValue(parameters) as string);
                }
            }

            var items = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return items;
        }

        public async Task<int> GetItemCountAsync(
            string searchQuery = null,
            object parameters = null)
        {
            var query = Data.AsQueryable();
            if (!(searchQuery is null))
            {
                var searchQueryLowercase = searchQuery.ToLower();
                query = query.Where(f => f.Date.ToString().ToLower().Contains(searchQueryLowercase)
                                    || f.TemperatureC.ToString().Contains(searchQuery)
                                    || f.TemperatureF.ToString().Contains(searchQuery)
                                    || f.Summary.ToLower().Contains(searchQueryLowercase));
            }

            if (!(parameters is null))
            {
                var type = parameters.GetType();
                if (!(type.GetProperty(nameof(WeatherForecast.Summary)) is null))
                {
                    query = query.Where(f => f.Summary == type.GetProperty(nameof(WeatherForecast.Summary)).GetValue(parameters) as string);
                }
            }

            return query.Count();
        }
    }
}
