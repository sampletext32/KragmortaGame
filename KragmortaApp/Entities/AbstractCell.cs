using KragmortaApp.Enums;

namespace KragmortaApp.Entities
{
    public class AbstractCell : VisualEntity
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
    }
}