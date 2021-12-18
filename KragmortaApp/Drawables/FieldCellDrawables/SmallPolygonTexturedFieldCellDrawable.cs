using System.Collections.Generic;
using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables.FieldCellDrawables
{
    public class SmallPolygonTexturedFieldCellDrawable : TexturedFieldCellDrawable
    {
        //TODO: Load first images and then init textures from them.
        public SmallPolygonTexturedFieldCellDrawable(FieldCell cell, int cellSize, Corner corner) : base(cell, cellSize)
        {
            _backgroundSprite       = new Sprite();
            _hoveredEffectRectangle = new Sprite();

            var textureIndex = random.Next(0, 3);

            switch (corner)
            {
                case Corner.TopLeft:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache($"smalltrapezeplate/smalltrapezeplate{textureIndex}");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache($"smalltrapezeplate/smalltrapezeplate{textureIndex}");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplateline");
                    break;
                }
                case Corner.TopRight:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplate{textureIndex}ref");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache($"smalltrapezeplate/smalltrapezeplate{textureIndex}ref");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplatelineref");
                    break;
                }
                case Corner.BottomLeft:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplate{textureIndex}refreversed");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplate{textureIndex}refreversed");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplatelinerefreversed");
                    break;
                }
                case Corner.BottomRight:
                {
                    _backgroundSprite.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplate{textureIndex}reversed");
                    _backgroundImage =
                        Engine.Instance.ImageCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplate{textureIndex}reversed");
                    _hoveredEffectRectangle.Texture =
                        Engine.Instance.TextureCache.GetOrCache(
                            $"smalltrapezeplate/smalltrapezeplatelinereversed");
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
            // _green = new RectangleShape()
            // {
            //     Size      = new Vector2f(10, 10),
            //     FillColor = Color.Green
            // };
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