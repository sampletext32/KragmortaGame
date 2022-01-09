using System.Collections.Generic;

namespace KragmortaApp.Entities
{
    public class Profile : VisualEntity
    {
        public string Nickname { get; set; }

        public int Lives { get; set; } = 3;

        public List<MagicBook> MagicBooks { get; set; }

        public Profile(string nickname)
        {
            Nickname = nickname;
            MagicBooks = new List<MagicBook>();
        }
    }
}