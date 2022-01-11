using System.Collections.Generic;
using KragmortaApp.Enums;

namespace KragmortaApp.FileDatas
{
    public class GameFieldFileData
    {
        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public List<FieldCellFileData> Cells { get; set; }

        public FieldType FieldType { get; set; }

        public int PlayersCount { get; set; }
    }
}