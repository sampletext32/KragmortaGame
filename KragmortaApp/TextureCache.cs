using System.Collections.Generic;
using SFML.Graphics;

namespace KragmortaApp
{
    public class TextureCache
    {
        private Dictionary<string, Texture> _textures;

        public TextureCache()
        {
            _textures = new();
        }

        public Texture GetOrCache(string name)
        {
            if (_textures.ContainsKey(name))
            {
                return _textures[name];
            }
            else
            {
                var texture = new Texture($"assets/textures/{name}.png");
                _textures[name] = texture;
                return texture;
            }
        }
    }
}