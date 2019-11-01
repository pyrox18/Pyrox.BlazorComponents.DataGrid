# Pyrox.BlazorComponents.DataGrid

[![](https://img.shields.io/nuget/v/Pyrox.BlazorComponents.DataGrid.svg?style=flat)](https://www.nuget.org/packages/Pyrox.BlazorComponents.DataGrid/)

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

### General usage (with example)

The instructions here are based on the weather forecast service provided in the default Blazor Server template. The code can be found in `tests/Pyrox.BlazorComponents.DataGrid.E2ETests`.

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

Implement the `IDataGridService<TItem>` interface, where `TItem` is the type of the data item (in this case, `WeatherForecast`). You may need to import the `Pyrox.BlazorComponents.DataGrid.Interfaces` namespace for this.

```cs
public class WeatherForecastService : IDataGridService<WeatherForecast>
{
    // This should be replaced with your actual data source
    private readonly List<WeatherForecast> Data = new List<WeatherForecast>();

    public async Task<DataGridResult<WeatherForecast>> GetItemsAsync(
        int pageNumber,
        int pageSize,
        SortInformation<WeatherForecast> sortInfo = null,
        string searchQuery = null,
        object parameters = null)
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

        if (!(parameters is null))
        {
            // Add logic for parameter handling here
        }

        // Get total item count before performing pagination
        var totalItems = (uint)query.Count();

        // Logic for pagination
        var items = query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new DataGridResult<WeatherForecast>(items, totalItems);
    }
}
```

`SortInformation<TItem>` is a class that contains:
- a `Key` property of type `string` that indicates the name of the property to sort the items by, and
- a `Type` property, which is a `SortType` enumeration that has two possible values: `Ascending` and `Descending`.

Use these values to determine the sort order for your fetched items and apply those accordingly in your service's logic.

Finally, use the `DataGrid` component in your Razor pages.

```razor
@page "/"

<h1>Weather Forecast</h1>

<DataGrid TItem="WeatherForecast"
          DefaultSort="SortInformation<WeatherForecast>.SortAscending(f => f.Date)"
          Parameters="parameters">
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

@code {
    private object parameters = new
    {
        Summary = "Balmy"
    }
}
```

Supply the `TItem` type parameter when declaring the component. 

The following parameters are optional:
- `DefaultSort`: Determines the default sort order for the fetched items. Use the `SortInformation<TItem>.SortAscending` or `SortInformation<TItem>.SortDescending` static methods to quickly get the `SortInformation<TItem>` instance that you want.
- `Parameters`: Parameters that you want to filter the results by. For example, supply an `OrderId` as a parameter and only fetch order items related to that `OrderId`. You are responsible for handling the presence/absence of these parameters in your `IDataGridService` implementation.

### Customise sort key display name

By default, the sort dropdown gets its key names from the property names of `TItem` and converts them into title case. If you would like to provide your own sort key display name, you can use the `SortKeyDisplayName` attribute on your `TItem` class properties.

```cs
public class WeatherForecast
{
    public DateTime Date { get; set; }

    [SortKeyDisplayName("Temperature (C)")]
    public int TemperatureC { get; set; }

    [SortKeyDisplayName("Temperature (F)")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; set; }
}
```

# Contributing

Refer to the CONTRIBUTING.md file for more information on how to contribute to this project.

# License

This library is licensed under the MIT license.
