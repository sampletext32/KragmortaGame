﻿using MainApp.Entities.Enums;

namespace MainApp.Entities.Models
{
    public class FieldCell
    {
        /// <summary>
        /// Field X coordinate
        /// </summary>
        public int X { get; set; }
        
        /// <summary>
        /// Field Y coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The type of current cell
        /// </summary>
        public FieldType Type { get; set; }
        
        /// <summary>
        /// Indicates, whether current cell is being under the mouse cursor
        /// </summary>
        public bool Hovered { get; set; }

        /// <summary>
        /// Indicates, whether current cell is being under click phase
        /// </summary>
        public bool Clicked { get; set; }

        public FieldCell()
        {
        }
    }
}