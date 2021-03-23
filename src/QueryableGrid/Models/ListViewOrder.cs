using QueryableGrid.Enums;

namespace QueryableGrid.Models
{
    public class ListViewOrder
    {
        public string Column { get; set; }
        public OrderTypeEnum Type { get; set; }
    }
}