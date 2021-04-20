using QGrid.Enums;

namespace QGrid.Models
{
    public class QGridOrder
    {
        public string Column { get; set; }
        public OrderTypeEnum Type { get; set; }

        public QGridOrder(string column, OrderTypeEnum type = OrderTypeEnum.Asc)
        {
            Column = column;
            Type = type;
        }
    }
}