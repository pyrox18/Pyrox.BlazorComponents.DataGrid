# Pyrox.BlazorComponents.DataGrid

[![Build Status](https://travis-ci.com/pyrox18/Pyrox.BlazorComponents.DataGrid.svg?branch=master)](https://travis-ci.com/pyrox18/Pyrox.BlazorComponents.DataGrid) [![](https://img.shields.io/nuget/v/Pyrox.BlazorComponents.DataGrid.svg?style=flat)](https://www.nuget.org/packages/Pyrox.BlazorComponents.DataGrid/)

An opinionated Blazor data grid component built on top of BlazorStrap.

## Installation

Download and install this package from NuGet using the Package Manager Console, .NET CLI or Visual Studio's NuGet Package Manager.

```bash
PM> Install-Package Pyrox.BlazorComponents.DataGrid
# OR
$ dotnet add package Pyrox.BlazorComponents.DataGrid
```

## Usage

**NOTE:** This component is built and tested with Blazor Server only. This component is not guaranteed to work with other versions of Blazor, such as Blazor WebAssembly.

The instructions here are based on the weather forecast service provided in the default Blazor server template. The code can be found in `tests/Pyrox.BlazorComponents.DataGrid.E2ETests`.

Assuming we have the following `WeatherForecast` entity:

```cs
public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string Summary { get; set; }
}
```

Create an `enum` that will be used as the sort key for the properties in `WeatherForecast`.

```cs
public enum WeatherForecastSortKey
{
    Date,
    TemperatureC,
    TemperatureF,
    Summary
}
```

Then, implement the `IDataGridService<TItem, TKey>` interface, where `TItem` is the type of the data item (in this case, `WeatherForecast`) and `TKey` is the type of the sort key (in this case, `WeatherForecastSortKey`). You may need to import the `Pyrox.BlazorComponents.DataGrid.Interfaces` namespace for this.

```cs
public class WeatherForecastService : IDataGridService<WeatherForecast, WeatherForecastSortKey>
{
    // This should be replaced with your actual data source
    private readonly List<WeatherForecast> Data = new List<WeatherForecast>();

    public async Task<List<WeatherForecast>> GetItemsAsync(
        int pageNumber,
        int pageSize,
        SortInformation<WeatherForecastSortKey> sortInfo = null,
        string searchQuery = null)
    {
        var query = Data.AsQueryable();

        if (!(sortInfo is null))
        {
            // Add logic for sorting here
        }

        if (!(searchQuery is null))
        {
            // Add logic for search queries here
        }

        // Logic for pagination
        var items = query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return items;
    }

    public async Task<int> GetItemCountAsync(string searchQuery = null)
    {
        if (!(searchQuery is null))
        {
            // Return count for items with search query
        }

        // Otherwise, return count for all items
    }
}
```

`SortInformation<TKey>` is a class that contains:
- a `Key` property of type `TKey` that indicates the item property to sort the items by, and
- a `Type` property, which is a `SortType` enumeration that has two possible values: `Ascending` and `Descending`.

Use these values to determine the sort order for your fetched items and apply those accordingly in your service's logic.

Finally, use the `DataGrid` component in your Razor pages.

```razor
@page "/"

<h1>Weather Forecast</h1>

<DataGrid TItem="WeatherForecast"
          TKey="WeatherForecastSortKey"
          DefaultSort="SortInformation<WeatherForecastSortKey>.SortAscending(WeatherForecastSortKey.Date)">
    <GridHeader>
        <th>Date</th>
        <th>Temperature (C)</th>
        <th>Temperature (F)</th>
        <th>Summary</th>
    </GridHeader>
    <GridRow>
        <td>@context.Date</td>
        <td>@context.TemperatureC</td>
        <td>@context.TemperatureF</td>
        <td>@context.Summary</td>
    </GridRow>
</DataGrid>
```

Supply the `TItem` and `TKey` type parameters when declaring the component. You can also optionally supply a `DefaultSort` parameter that determines the default sort order for the fetched items. Use the `SortInformation<TKey>.SortAscending` or `SortInformation<TKey>.SortDescending` static methods to quickly get the `SortInformation<TKey>` instance that you want.

# Contributing

Refer to the CONTRIBUTING.md file for more information on how to contribute to this project.

# License

This library is licensed under the MIT license.
