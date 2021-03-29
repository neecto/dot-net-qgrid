using System.Collections.Generic;
using System.Linq;
using QGrid.Enums;

namespace QGrid.Models
{
    public class QGridFilters
    {
        public FilterOperatorEnum Operator { get; set; }
        public IList<QGridFilter> Filters { get; set; }

        public QGridFilters() { }

        public QGridFilters(FilterOperatorEnum op, IList<QGridFilter> filters)
        {
            Operator = op;
            Filters = filters;
        }

        public QGridFilters(FilterOperatorEnum op, string column, FilterConditionEnum condition, object value)
        {
            Operator = op;
            Filters = new List<QGridFilter>
            {
                new QGridFilter(column, condition, value)
            };
        }

        public QGridFilters(FilterOperatorEnum op, string column, FilterConditionEnum condition, IEnumerable<object> values)
        {
            Operator = op;
            Filters = values
                .Select(x => new QGridFilter(column, condition, x))
                .ToList();
        }
    }
}