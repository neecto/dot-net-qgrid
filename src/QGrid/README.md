# QGrid

## Summary
Querying data with QGrid is really simple. All you need to do, is to generate a valid **QGridRequest** object that represents how you want the data to be paged, sorted, and filtered, and pass this object as an argument to `ToQGridResult()` extension method that you would invoke on an `IQueryable<T>` object. This method will translate the request into SQL, execute it, and return a **QGridResult** object.

For filtering data, you need to specify the name of a property of the IQueryable object, filter condition, and a value for this condition. QGrid supports filtering by any primitive data type that is supported by EntityFramwork as a database table column type. For the whole list of types see [Filter Conditions](#-filter-conditions)

For ordering, you need to specify the name of the property to order by and the ordering type (ascending or descending).


| NOTE: |
| :--- |
| QGrid library can work with any version of Entity Framework or Entity Framwork Core because it has no external dependecies. However, due to that it does not implement async method. For that, you can use [QGrid.EntityFrameworkCore](https://github.com/neecto/dot-net-qgrid/tree/master/src/QGrid.EntityFrameworkCore)|

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
| QGridFilters | QGridFilters | See [QGridFilters](#-qgridfilters) |
| Ordering | QGridFilters | See [QGridOrder](#-qgridorder) |

##  QGridFilters

### QGridFilters Type
| Property | Type | Description |
| :--- | :--- | :--- |
| Operator | FilterOperatorEnum | The logical operator that is used to combine filters. Values: `And`, `Or`  |
| Filters | IList<QGridFilter> | The list of filters to be applied |
  
### QGridFilter Type
| Property | Type | Description |
| :--- | :--- | :--- |
| Column | string | Name of a column that needs to be filtered  |
| Condition | FilterConditionEnum | Type of filtering (see [Filter Conditions](#-filter-conditions)) |
| Value | object | The value to filter with. Should have the same type as the filtered column |

### Filter Conditions
Filter conditions that can be applied to a property of a queried object depend on the property type. In case when a filter condition is not supported for a property type, `ArgumentOutOfRangeException` will be thrown by `ToQGridResult()` or `ApplyFilters()` method. Here's the list of data types and filter conditions supported by QGrid:

#### `bool` and `bool?`
| Filter Condition | Description |
| :--- | :--- |
| Eq | Equal to filter value |
| Neq | Not equal to filter value |

#### `DateTime` and `DateTime?`
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
| Lt | Less (earlier in time) than provided filter value with only Date part of DateTime value compared |
| Gt | Greater (later in time) than provided filter value with only Date part of DateTime value compared |
| Lte | Less (earlier in time) than or equal to provided filter value with only Date part of DateTime value compared |
| Gte | Greater (later in time) than or equal to provided filter value with only Date part of DateTime value compared |

| NOTE: |
| :--- |
| Filter conditions with *date* postfix, like `Eqdate` are useful when you need to disregard the Time part of a DateTime value for filtering. For example, when you send a filter object with `Eqdate` condition and value *[5/4/2021 3:14:16 PM]*, records that have values with different time but the same date will still be included. |


##  QGridOrder

## Example JSON
