using System.Collections.Generic;

namespace KragmortaApp.FileDatas
{
    public class ProfileFileData
    {
        public string Nickname { get; set; }

        public bool Activated { get; set; }

        public int Lives { get; set; }

        public List<MagicBookFileData> MagicBooks { get; set; }
    }
}