using System.Collections.Generic;
using System.Linq;
using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities
{
    public class Profile : VisualEntity
    {
        public string Nickname { get; set; }

        public bool Activated { get; set; }

        public int Lives { get; set; } = 3;

        public List<MagicBook> MagicBooks { get; set; }

        public Profile(ProfileFileData fileData)
        {
            Nickname   = fileData.Nickname;
            Activated  = fileData.Activated;
            Lives      = fileData.Lives;
            MagicBooks = fileData.MagicBooks.Select(f => new MagicBook(f)).ToList();
        }

        public ProfileFileData ToFileData()
        {
            return new ProfileFileData()
            {
                Activated  = Activated,
                Lives      = Lives,
                Nickname   = Nickname,
                MagicBooks = MagicBooks.Select(m => m.ToFileData()).ToList()
            };
        }

        public Profile(string nickname)
        {
            Nickname   = nickname;
            MagicBooks = new List<MagicBook>();
        }
    }
}