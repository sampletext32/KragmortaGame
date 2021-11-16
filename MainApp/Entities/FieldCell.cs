using MainApp.Enums;

namespace MainApp.Entities
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
        public CellType Type { get; set; }
        
        /// <summary>
        /// Indicates, whether current cell is being under the mouse cursor
        /// </summary>
        public bool Hovered { get; set; }

        /// <summary>
        /// Indicates, whether current cell is being under click phase
        /// </summary>
        public bool Clicked { get; set; }
        
        /// <summary>
        /// Indicates, whether hero can move to current cell 
        /// </summary>
        public bool IsPossibleMoveTarget { get; set; }

        public FieldCell()
        {
        }
    }
}