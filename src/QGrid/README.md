# QGrid

## Summary
Querying data with QGrid is really simple. All you need to do, is to generate a valid **QGridRequest** object that represents how you want the data to be paged, sorted, and filtered, and pass this object as an argument to `ToQGridResult()` extension method that you would invoke on an `IQueryable<T>` object. This method will translate the request into SQL, execute it, and return a **QGridResult** object.

For filtering data, you need to specify the name of a property of the IQueryable object, filter condition, and a value for this condition. QGrid supports filtering by any primitive data type that is supported by EntityFramwork as a database table column type. For the whole list of types see [Filter Conditions](#filter-conditions).

For ordering, you need to specify the name of the property to order by and the ordering type (ascending or descending).


| NOTE: |
| :--- |
| QGrid library can work with any version of Entity Framework or Entity Framwork Core because it has no external dependencies. However, due to that, it does not implement an async method for executing the request. For that, you can use [QGrid.EntityFrameworkCore](https://github.com/neecto/dot-net-qgrid/tree/master/src/QGrid.EntityFrameworkCore) or just copy the [extension method](https://github.com/neecto/dot-net-qgrid/blob/master/src/QGrid.EntityFrameworkCore/QueryableExtensions.cs) to your project if you have older Entity Framework version.|

## Public Methods
Besides `ToQGridResult()` QGrid provides two more IQueryable extension methods in case you'd like to perform separate sorting or filtering:
- `ApplyOrdering()` which accepts [QGridOrder model](#qgridorder) 
- `ApplyFilters()` which accepts [QGridFilters model](#qgridfilters)

Both of these methods return `IQueryable<T>` where `T` is the type of your original projection model.
Also, because QGrid by it's own does not implement async methods, you can use `ApplyOrdering()` and `ApplyFilters()` to implement your own async version of `ToQGridResult()`. However, unless you are using a pre-core version of Entity Framework, you can just call `ToQGridResultAsync()` method from **QGrid.EntityFrameworkCore** library.

## QGridRequest Model

| Property | Type | Description |
| :--- | :--- | :--- |
| PageSize | int | The amount of records to be displayed on a grid page |
| PageNumber | int | The page number to display (starting from 1) |
| QGridFilters | Filters | See [QGridFilters](#qgridfilters) |
| Ordering | Ordering | See [QGridOrder](#qgridorder) |

##  QGridFilters

### QGridFilters Type
| Property | Type | Description |
| :--- | :--- | :--- |
| Operator | FilterOperatorEnum | The logical operator that is used to combine filters. Values: `And`, `Or`  |
| Filters | IList\<QGridFilter\> | The list of filters to be applied |
  
### QGridFilter Type
| Property | Type | Description |
| :--- | :--- | :--- |
| Column | string | Name of a column that needs to be filtered  |
| Condition | FilterConditionEnum | Type of filtering (see [Filter Conditions](#filter-conditions)) |
| Value | object | The value to filter with. Should have the same type as the filtered column |

### Filter Conditions
Filter conditions which can be applied to a property of a queried object depend on the filtered property type. In case when a filter condition is not supported for a property type, `ArgumentOutOfRangeException` will be thrown by `ToQGridResult()` or `ApplyFilters()` method. Here's the list of data types and filter conditions supported by QGrid:

#### `bool`
| Filter Condition | Description |
| :--- | :--- |
| Eq | Equal to filter value |
| Neq | Not equal to filter value |

#### `DateTime`
| Filter Condition | Description |
| :--- | :--- |
| Eq | Equal to filter value |
| Neq | Not equal to filter value |
| Lt | Less (earlier in time) than provided filter value |
| Gt | Greater (later in time) than provided filter value |
| Lte | Less (earlier in time) than or equal to provided filter value |
| Gte | Greater (later in time) than or equal to provided filter value |
| Eqdate | Equal to filter value with only Date part of DateTime value compared |
| Neqdate | Not equal to filter value with only Date part of DateTime value compared |
| Ltdate | Less (earlier in time) than provided filter value with only Date part of DateTime value compared |
| Gtdate | Greater (later in time) than provided filter value with only Date part of DateTime value compared |
| Ltedate | Less (earlier in time) than or equal to provided filter value with only Date part of DateTime value compared |
| Gtedate | Greater (later in time) than or equal to provided filter value with only Date part of DateTime value compared |

| NOTE: |
| :--- |
| Filter conditions with *date* postfix, like `Eqdate` are useful when you need to disregard the Time part of a DateTime value for filtering. For example, when you send a filter object with `Eqdate` condition and value *[5/4/2021 3:14:16 PM]*, records that have values with different time but the same date will still be included. |

#### Enums
| Filter Condition | Description |
| :--- | :--- |
| Eq | Equal to filter value |
| Neq | Not equal to filter value |
| Oneof | Equal to one of the provided values |
| Notoneof | Not equal to any of the provided values |


| NOTE: |
| :--- |
| The filter value that is provided for filtering by a property that is an Enum can be either a string or an integer. In any case, it should be a valid value within the Enum type that the filtered property has, otherwise QGrid will throw the `OverflowException`. |

| NOTE: |
| :--- |
| `Oneof` and `Notoneof` filter conditions require a collection object as a filter value (array in JSON, object that implements IEnumerable in .NET). This operator is helpful when you need to compare the column value to a list of values. |

#### `Guid`
| Filter Condition | Description |
| :--- | :--- |
| Eq | Equal to filter value |
| Neq | Not equal to filter value |

#### Numbers
QGrid supports the following types for properties with number values: `int`, `long`, `decimal`, and `double`. And the following filter conditions:

| Filter Condition | Description |
| :--- | :--- |
| Eq | Equal to filter value |
| Neq | Not equal to filter value |
| Lt | Less than provided filter value |
| Gt | Greater than provided filter value |
| Lte | Less than or equal to provided filter value |
| Gte | Greater than or equal to provided filter value |

#### `string`
| Filter Condition | Description |
| :--- | :--- |
| Eq | Equal to filter value |
| Neq | Not equal to filter value |
| Contains | String contains the filter value |
| Doesnotcontain | String does not contain the filter value |
| Startswith | String starts with the filter value |
| Endswith | String ends with the filter value |

#### Nullable Columns
QGrid supports nullable columns for any value type and any filter condition. You can also pass `null` as a filter value, however, in this case you can only use *Eq* and *Neq* filter conditions, otherwise QGrid will throw the `ArgumentOutOfRangeException`. Other than that, passing `null` as a filter value for *Eq* or *Neq* will work like in other case, simply comparing the value to null.

##  QGridOrder
| Property | Type | Description |
| :--- | :--- | :--- |
| Column | string | Name of a column to order by  |
| Type | OrderTypeEnum | Type of ordering. Possible values: OrderTypeEnum.Asc, OrderTypeEnum.Desc |

| NOTE: |
| :--- |
| When multiple QGridOrder objects exist in `Ordering` collection of a QGridRequest object, the ordering operations will be applied according to the order of the items in that collection |

## QGridResult
QGridResult<T> model represent a response that `.ToQGridResult()` method produces, it's generic argument value matches the generic type of `IQueryable<T>` which the method is executed on.
  
| Property | Type | Description |
| :--- | :--- | :--- |
| PageNumber | int | Page number that is being returned. Mathes the PageNumber of QGridRequest  |
| ItemsOnPage | int | Amount of records the grid page actually contains |
| PagesTotal | int | Total number of pages that contain records with current filtering |
| Total | int | Total amount of records before filtering |
| TotalFiltered | int | The amount of records after filtering |
| Items | IList\<T\> | Result records |

## Example JSON requests

### Simple request with no filtering or ordering that shows 10 items per page on the first page
```json
{
  'pageSize': 10,
  'pageNumber': 1
}
```

### Request with multiple orderings
```json
{
  'pageSize': 10,
  'pageNumber': 1,
  'ordering': [
    {
      'column': 'dateOfBirth',
      'type': 'desc'
    },
    {
      'column': 'salary',
      'type': 'asc'
    }
  ]
}
```

### Request with ordering and filters
```json
{
  'pageSize': 10,
  'pageNumber': 1,
  'ordering': [
    {
      'column': 'dateOfBirth',
      'type': 'desc'
    }
  ],
  'filters': {
    'operator': 'and',
    'filters': [
      {
        'column': 'title',
        'condition': 'contains',
        'value: 'dev'
      },
      {
        'column': 'salary',
        'condition': 'lte',
        'value: '1000'
      },
      {
        'column': 'level',
        'condition': 'oneof',
        'value: ['lead', 'senior']
      }
    ]
  }
}
```
