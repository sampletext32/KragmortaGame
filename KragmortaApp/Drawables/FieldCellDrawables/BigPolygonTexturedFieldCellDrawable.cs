using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables.FieldCellDrawables
{
    public class BigPolygonTexturedFieldCellDrawable : TexturedFieldCellDrawable
    {
        public BigPolygonTexturedFieldCellDrawable(FieldCell cell, int cellSize, Corner corner) : base(cell, cellSize)
        {
            _backgroundSprite       = new Sprite();
            _hoveredEffectRectangle = new Sprite();

            var textureIndex = random.Next(0, 3);

            switch (corner)
            {
                case Corner.TopLeft:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache($"bigtrapezeplate/bigtrapezeplate{textureIndex}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache($"bigtrapezeplate/bigtrapezeplate{textureIndex}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/bigtrapezeplateline");
                    break;
                }
                case Corner.TopRight:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache($"bigtrapezeplate/bigtrapezeplate{textureIndex}ref");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache($"bigtrapezeplate/bigtrapezeplate{textureIndex}ref");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/bigtrapezeplatelineref");
                    break;
                }
                case Corner.BottomLeft:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/bigtrapezeplate{textureIndex}refreversed");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"bigtrapezeplate/bigtrapezeplate{textureIndex}refreversed");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/bigtrapezeplatelinerefreversed");
                    break;
                }
                case Corner.BottomRight:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/bigtrapezeplate{textureIndex}reversed");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache($"bigtrapezeplate/bigtrapezeplate{textureIndex}reversed");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"bigtrapezeplate/bigtrapezeplatelinereversed");
                    break;
                }
                default:
                {
                    _backgroundSprite.Color = Color.Red;
                    break;
                }
            }

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