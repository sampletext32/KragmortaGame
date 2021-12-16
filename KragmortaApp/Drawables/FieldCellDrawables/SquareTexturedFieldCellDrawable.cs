using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables.FieldCellDrawables
{
    public class SquareTexturedFieldCellDrawable : TexturedFieldCellDrawable
    {
        public SquareTexturedFieldCellDrawable(FieldCell cell, int cellSize) : base(cell, cellSize)
        {
            _backgroundSprite       = new Sprite();
            _hoveredEffectRectangle = new Sprite();

            var textureIndex = random.Next(0, 3);


            _backgroundSprite.Texture =
                Engine.Instance.TextureCache.GetOrCache($"squareplate/squareplate{textureIndex}");
            _backgroundImage = Engine.Instance.ImageCache.GetOrCache($"squareplate/squareplate{textureIndex}");
            _hoveredEffectRectangle.Texture =
                Engine.Instance.TextureCache.GetOrCache($"squareplate/squareplateline");

            var downscaleFactor = (float)cellSize / _backgroundSprite.Texture.Size.X;
            _hoveredEffectRectangle.Scale = _backgroundSprite.Scale = new Vector2f(downscaleFactor, downscaleFactor);


            _red = new RectangleShape()
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Red
            };
            _green = new RectangleShape()
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Green
            };
            _blue = new RectangleShape()
            {
                Size      = new Vector2f(10, 10),
                FillColor = Color.Blue
            };
            _orange = new RectangleShape()
            {
                Size      = new Vector2f(10, 10),
                FillColor = new Color(255, 165, 0)
            };
        }
    }
}