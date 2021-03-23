using System.ComponentModel;

namespace QGrid.Enums
{
    public enum FilterConditionEnum
    {
        [Description("Equal")]
        Eq = 1,

        [Description("Not equal")]
        Neq,

        [Description("Contains")]
        Contains,

        [Description("Starts With")]
        Startswith,

        [Description("Ends With")]
        Endswith,

        [Description("Does not contain")]
        Doesnotcontain,

        [Description("Less than")]
        Lt,

        [Description("Greater than")]
        Gt,

        [Description("Less than or equal")]
        Lte,

        [Description("Greater than or equal")]
        Gte,

        [Description("Equal date")]
        Eqdate,

        [Description("Not equal date")]
        Neqdate,

        [Description("Less than date")]
        Ltdate,

        [Description("Greater than date")]
        Gtdate,

        [Description("Less or equal date")]
        Ltedate,

        [Description("Greater or equal date")]
        Gtedate,

        [Description("One of the values")]
        Oneof,

        [Description("Not one of the values")]
        Notoneof
    }
}