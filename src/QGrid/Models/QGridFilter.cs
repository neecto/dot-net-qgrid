using QGrid.Enums;

namespace QGrid.Models
{
    public class QGridFilter
    {
        public string Column { get; set; }
        public FilterConditionEnum Condition { get; set; }
        public object Value { get; set; }

        public QGridFilter(string column, FilterConditionEnum condition, object value)
        {
            Column = column;
            Condition = condition;
            Value = value;
        }

        public QGridFilter() { }
    }
}