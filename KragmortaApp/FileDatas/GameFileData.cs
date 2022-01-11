using System.Collections.Generic;
using KragmortaApp.Entities;

namespace KragmortaApp.FileDatas
{
    public class GameFileData
    {
        public int HeroCount { get; set; }

        public int CurrentPlayerIndex { get; set; }

        public List<HeroFileData> Heroes { get; set; }

        public RigorFileData Rigor { get; set; }

        public List<ProfileFileData> Profiles { get; set; }

        public GameFieldFileData Field { get; set; }

        public PathFileData Path { get; set; }

        public PushFileData Push { get; set; }

        public PushedStateFileData PushedState { get; set; }

        public PushedStateFileData PushedStateModel { get; set; }
    }
}