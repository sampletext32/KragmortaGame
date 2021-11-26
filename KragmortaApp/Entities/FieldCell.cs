namespace KragmortaApp.Entities
{
    public class FieldCell : AbstractCell
    {
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