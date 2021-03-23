using QueryableGrid.Enums;

namespace QueryableGrid.Models
{
    public class ListViewFilter
    {
        public string Column { get; set; }
        public FilterConditionEnum Condition { get; set; }
        public object Value { get; set; }
    }
}