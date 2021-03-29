using QGrid.Enums;

namespace QGrid.Models
{
    public class QGridFilter
    {
        public string Column { get; set; }
        public FilterConditionEnum Condition { get; set; }
        public object Value { get; set; }
    }
}