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

        /// <summary>
        /// Loads up a texture given by the path or return already cached one.
        /// </summary>
        /// <param name="name">Path to the image WITHOUT extenstion. Only .png files!</param>
        /// <returns></returns>
        public Texture GetOrCache(string name)
        {
            if (_textures.ContainsKey(name))
            {
                return _textures[name];
            }
            else
            {
                var texture = new Texture(Engine.Instance.ImageCache.GetOrCache(name));
                _textures[name] = texture;
                return texture;
            }
        }
    }
}