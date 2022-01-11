using System.Collections.Generic;
using SFML.Graphics;

namespace KragmortaApp
{
    public class FontCache
    {
        private Dictionary<string, Font> _fonts;

        public FontCache()
        {
            _fonts = new();
        }

        public Font GetOrCache(string name)
        {
            if (_fonts.ContainsKey(name))
            {
                return _fonts[name];
            }
            else
            {
                var font = new Font($"assets/fonts/{name}.ttf");
                _fonts[name] = font;
                return font;
            }
        }
    }
}