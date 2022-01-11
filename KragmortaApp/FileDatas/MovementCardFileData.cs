using KragmortaApp.Enums;

namespace KragmortaApp.FileDatas
{
    public class MovementCardFileData
    {
        public CellType FirstType { get; set; }
        public CellType SecondType { get; set; }

        public bool Selected { get; set; }

        public bool HasUsedFirstType { get; set; }
        public bool HasUsedSecondType { get; set; }
        public bool Activated { get; set; }
    }
}