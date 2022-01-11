using System.Collections.Generic;
using KragmortaApp.Entities;

namespace KragmortaApp.FileDatas
{
    public class GameFileData
    {
        public int HeroCount { get; set; }

        public List<HeroFileData> Heroes { get; set; }

        public RigorFileData Rigor { get; set; }

        public List<ProfileFileData> Profiles { get; set; }
        
        public GameFieldFileData Field { get; set; }
    }
}