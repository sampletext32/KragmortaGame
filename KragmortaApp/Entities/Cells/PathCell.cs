using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities.Cells
{
    public class PathCell : AbstractCell
    {
        public PathCell()
        {
        }

        public PathCell(PathCellFileData fileData)
        {
            X        = fileData.X;
            Y        = fileData.Y;
            IsPortal = fileData.IsPortal;
            Corner   = fileData.Corner;
            Form     = fileData.Form;
            Type     = fileData.Type;
            Visible  = fileData.Visible;
        }

        public PathCellFileData ToFileData()
        {
            return new PathCellFileData()
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