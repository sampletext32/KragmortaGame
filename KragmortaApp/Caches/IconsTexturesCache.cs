using System;
using System.Collections.Generic;
using System.ComponentModel;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Caches
{
    public class IconsTexturesCache
    {
        private Dictionary<string, Texture> _sprites;

        private Random _random;

        public IconsTexturesCache()
        {
            _sprites = new();
            _random  = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Loads up a sprite given by the name or return already cached one.
        /// </summary>
        /// <param name="cellType">Type of the cell to retrieve.</param>
        /// <returns></returns>
        public Texture GetOrCache(CellType cellType)
        {
            if (!Enum.IsDefined(typeof(CellType), cellType))
                throw new InvalidEnumArgumentException(nameof(cellType), (int)cellType, typeof(CellType));

            if (_sprites.ContainsKey(nameof(cellType)))
            {
                return _sprites[nameof(cellType)];
            }

            var sprite = new Sprite(Engine.Instance.TextureCache.GetOrCache(Enum.GetName(typeof(CellType), cellType)));
            _sprites[nameof(cellType)] = SetUpTexture(cellType);
            // return sprite;
            return null;
        }

        private Texture SetUpTexture(CellType cellType)
        {
            // switch (cellType)
            // {
            //     case CellType.Orange:
            //         return InitOrangeIcon();
            //
            //     case CellType.Blue:
            //         return InitBlueIcon();
            //     
            //     case CellType.Green:
            //         return InitGreenIcon();
            //     
            //     case CellType.Red:
            //         return InitRedIcon();
            //
            //     case CellType.Common:
            //         return InitCommonIcon();
            //
            //     default: return null;
            // }
            return null;
        }

        private Texture InitOrangeIcon()
        {
            var orange = new Texture(Engine.Instance.TextureCache.GetOrCache(nameof(CellType.Orange)));

            return orange;
        }

        private Sprite InitBlueIcon()
        {
            var blue = new Sprite(Engine.Instance.TextureCache.GetOrCache(nameof(CellType.Blue)));
            blue.Scale    = new Vector2f(0.65f, 0.65f);
            blue.Rotation = _random.Next(2) % 2 == 0 ? -15f : 15f;
            
            return blue;
        }

        private Sprite InitGreenIcon()
        {
            var green = new Sprite(Engine.Instance.TextureCache.GetOrCache(nameof(CellType.Green)));
            green.Scale    = new Vector2f(0.6f, 0.6f);
            green.Rotation = _random.Next(2) % 2 == 0 ? -15f : 15f;

            return green;
        }

        private Sprite InitRedIcon()
        {
            var red = new Sprite(Engine.Instance.TextureCache.GetOrCache(nameof(CellType.Red)));
            red.Scale    = new Vector2f(0.5f, 0.5f);
            red.Rotation = _random.Next(2) % 2 == 0 ? -15f : 15f;

            return red;
        }

        private Sprite InitCommonIcon()
        {
            var common = new Sprite(Engine.Instance.TextureCache.GetOrCache(nameof(CellType.Common)));
            common.Scale = new Vector2f(0.5f, 0.5f);

            return common;
        }
    }
}