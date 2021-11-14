﻿using System;

namespace MainApp.Entities.Enums
{
    [Flags]
    public enum FieldType
    {
        Empty = 0,
        Red = 1,
        Green = 2,
        Blue = 4,
        Orange = 8
    }
}