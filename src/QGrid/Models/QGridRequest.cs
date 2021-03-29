using System.Collections.Generic;

namespace QGrid.Models
{
    public class QGridRequest
    {
        public IList<QGridOrder> Ordering { get; set; }
        public IList<QGridFilter> Filters { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}