# QGrid

## What's QGrid?
QGrid is a small but powerful library build for .NET Core for server-side paging, sorting, and filtering data for grids, lists, tables, etc. Filtering and sorting operations are performed on the database level using Entity Framework and Linq Expressions, so it is required for QGrid to work.

## NuGet Packages

You can download QGrid as NuGet here:
<links>
  
## QGrid and QGrid.EntityFrameworkCore

QGrid consists of two NuGet packages: QGrid and QGrid.EntityFrameworkCore. The base QGrid package targets **.NET Standard** framework and has no external dependencies at all; while QGrid.EntityFrameworkCore package, as the name suggests, depends on **Microsoft.EntityFrameworkCore**. That's made in order to allow using QGrid with any version of EntityFramwork (even pre-core). However, QGrid does not expose an async method for querying the data, so take a look at the [docs]() to see how to work with it.

## Example

Here's a simple example of how to use QGrid:
```c#
  // of course, usually you would deserialize the QGridRequest object 
  // from JSON that's coming into your API endpoint handler
  var request = new QGridRequest
  {
      Ordering = new List<QGridOrder>
      {
          new QGridOrder("DateOfBirth")
      },
      QGridFilters = new QGridFilters
      {
          Operator = FilterOperatorEnum.And,
          Filters = new List<QGridFilter>
          {
              new QGridFilter("Title", FilterConditionEnum.Contains, "dev"),
              new QGridFilter("ExperienceYears", FilterConditionEnum.Gt, 10)
          }
      },
      PageSize = 10,
      PageNumber = 1
  };
  
  var employees = _dbContext.Employees.ToQGridResult(request);
```

## Docs
