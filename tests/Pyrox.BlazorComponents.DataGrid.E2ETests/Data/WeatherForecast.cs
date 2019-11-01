using Pyrox.BlazorComponents.DataGrid.Attributes;
using System;

namespace Pyrox.BlazorComponents.DataGrid.E2ETests.Data
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        [SortKeyDisplayName("Temperature (C)")]
        public int TemperatureC { get; set; }

        [SortKeyDisplayName("Temperature (F)")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
