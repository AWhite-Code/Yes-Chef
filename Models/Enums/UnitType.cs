using System.ComponentModel;

namespace Yes_Chef.Models.Enums
{
    public enum UnitType
    {
        [Description("g")]
        Gram,

        [Description("kg")]
        Kilogram,

        [Description("ml")]
        Milliliter,

        [Description("tsp")]
        Teaspoon,

        [Description("tbsp")]
        Tablespoon,

        [Description("cup")]
        Cup,

        [Description("pcs")]
        Piece
    }
}
