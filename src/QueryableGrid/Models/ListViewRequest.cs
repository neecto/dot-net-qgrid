using System.Collections.Generic;

namespace QueryableGrid.Models
{
    public class ListViewRequest
    {
        public IList<ListViewOrder> Ordering { get; set; }
        public IList<ListViewFilter> Filters { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}