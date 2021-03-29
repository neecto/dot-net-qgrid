using System.Collections.Generic;

namespace QGrid.Models
{
    public class QGridResult<T> where T : class
    {
        public int PageNumber { get; set; }
        public int ItemsOnPage { get; set; }
        public int PagesTotal { get; set; }
        public int Total { get; set; }
        public int TotalFiltered { get; set; }
        public IList<T> Items { get; set; }
    }
}