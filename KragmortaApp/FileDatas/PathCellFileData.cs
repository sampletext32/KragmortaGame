using KragmortaApp.Enums;

namespace KragmortaApp.FileDatas
{
    public class PathCellFileData
    {
        public int X { get; set; }
        
        public int Y { get; set; }
        
        public CellType Type { get; set; }
        
        public CellForm Form { get; set; }

        public Corner Corner { get; set; }
        
        public bool Visible { get; set; }
        
        public bool IsPortal { get; set; }
    }
}