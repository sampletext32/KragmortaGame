using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities.Cells
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

        public FieldCell(FieldCellFileData fileData)
        {
            X        = fileData.X;
            Y        = fileData.Y;
            IsPortal = fileData.IsPortal;
            Corner   = fileData.Corner;
            Form     = fileData.Form;
            Type     = fileData.Type;
            Visible  = fileData.Visible;
        }

        public FieldCellFileData ToFileData()
        {
            return new FieldCellFileData()
            {
                X        = X,
                Y        = Y,
                IsPortal = IsPortal,
                Corner   = Corner,
                Form     = Form,
                Type     = Type,
                Visible  = Visible
            };
        }
    }
}