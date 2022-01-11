using System;

namespace KragmortaApp.Enums
{
    [Flags]
    public enum CellType
    {
        Empty = 0,
        Red = 1,
        Green = 2,
        Blue = 4,
        Orange = 8,
        Common = 16,
        Wall = 32
    }
}