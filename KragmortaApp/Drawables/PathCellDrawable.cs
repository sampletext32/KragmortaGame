using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class PathCellDrawable : AbstractCellDrawable
    {
        private readonly int _cellSize;

        public PathCellDrawable(PathCell pathCell, int cellSize) : base(pathCell, cellSize,
            GameState.Instance.ColorsStorage.PathCell)
        {
            _cellSize = cellSize;
        }

        public void LoadTexture()
        {
            switch (_cell.Form)
            {
                case CellForm.Big:
                    LoadBigTexture(_cell.Corner);
                    break;

                case CellForm.Small:
                    LoadSmallTexture(_cell.Corner);
                    break;

                case CellForm.Square:
                    LoadSquareTexture();
                    break;
            }
        }

        protected override void LoadBigTexture(Corner corner)
        {
            _effectSprite.Texture = corner switch
            {
                Corner.TopLeft => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlate/bigtrapezeplateline_path"),
                Corner.TopRight => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateRef/bigtrapezeplatelineref_path"),
                Corner.BottomLeft => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateRefReversed/bigtrapezeplatelinerefreversed_path"),
                Corner.BottomRight => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateReversed/bigtrapezeplatelinereversed_path"),
                Corner.None => null,
                _ => throw new KragException("Unknown Form of the cell.")
            };
            
            InitBig(corner, _cellSize);
        }

        protected override void LoadSmallTexture(Corner corner)
        {
            _effectSprite.Texture = corner switch
            {
                Corner.TopLeft => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlate/smalltrapezeplateline_path"),
                Corner.TopRight => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateRef/smalltrapezeplatelineref_path"),
                Corner.BottomLeft => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateRefReversed/smalltrapezeplatelinerefreversed_path"),
                Corner.BottomRight => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateReversed/smalltrapezeplatelinereversed_path"),
                Corner.None => null,
                _ => throw new KragException("Unknown Form of the cell.")
            };
            
            InitSmall(corner, _cellSize);
        }

        protected override void LoadSquareTexture()
        {
            _effectSprite.Texture =
                Engine.Instance.TextureCache.GetOrCache($"squareplate/squareplateline_path");
            
            InitSquare(_cellSize);
        }
    }
}