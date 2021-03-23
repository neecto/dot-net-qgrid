using System;

namespace QGrid.Tests.Models
{
    public class TestItem
    {
        public int IntColumn { get; set; }
        public int? IntNullableColumn { get; set; }
        public string StringColumn { get; set; }
        public bool BoolColumn { get; set; }
        public bool? BoolNullableColumn { get; set; }
        public DateTime DateTimeColumn { get; set; }
        public DateTime? DateTimeNullableColumn { get; set; }
        public decimal DecimalColumn { get; set; }
        public decimal? DecimalNullableColumn { get; set; }
        public TestEnum EnumColumn { get; set; }
        public TestEnum? EnumNullableColumn { get; set; }
        public Guid GuidColumn { get; set; }
        public Guid? GuidNullableColumn { get; set; }
    }
}