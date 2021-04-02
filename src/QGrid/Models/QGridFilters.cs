using System.Collections.Generic;
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
    }
}