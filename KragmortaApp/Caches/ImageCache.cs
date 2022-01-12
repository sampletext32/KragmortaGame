using System.Collections.Generic;
using SFML.Graphics;

namespace KragmortaApp.Caches
{
    public class ImageCache
    {
        private Dictionary<string, Image> _images;

        public ImageCache()
        {
            _images = new();
        }

        public Image GetOrCache(string name)
        {
            if (_images.ContainsKey(name))
            {
                return _images[name];
            }
            else
            {
                var image = new Image($"assets/textures/{name}.png");
                _images[name] = image;
                return image;
            }
        }
    }
}