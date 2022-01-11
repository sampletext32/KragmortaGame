using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities.Cells
{
    public class PushCell : AbstractCell
    {
        // public bool Visible { get; set; }

        public PushCell()
        {
        }

        public PushCell(PushCellFileData fileData)
        {
            X        = fileData.X;
            Y        = fileData.Y;
            IsPortal = fileData.IsPortal;
            Corner   = fileData.Corner;
            Form     = fileData.Form;
            Type     = fileData.Type;
            Visible  = fileData.Visible;
        }

        public PushCellFileData ToFileData()
        {
            return new PushCellFileData()
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