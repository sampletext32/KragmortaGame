using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;

namespace KragmortaApp.Drawables
{
    public class PortalCellDrawable : AbstractCellDrawable
    {
        private readonly int _cellSize;

        public PortalCellDrawable(AbstractCell cell, int cellSize) : base(cell, cellSize,
            GameState.Instance.ColorsStorage.PortalCell)
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
                    $"bigtrapezeplate/BigTrapezePlate/bigtrapezeplateline_portal_blue"),
                Corner.TopRight => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateRef/bigtrapezeplatelineref_portal_blue"),
                Corner.BottomLeft => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateRefReversed/bigtrapezeplatelinerefreversed_portal_blue"),
                Corner.BottomRight => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateReversed/bigtrapezeplatelinereversed_portal_blue"),
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
                    $"smalltrapezeplate/SmallTrapezePlate/smalltrapezeplateline_portal_blue"),
                Corner.TopRight => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateRef/smalltrapezeplatelineref_portal_blue"),
                Corner.BottomLeft => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateRefReversed/smalltrapezeplatelinerefreversed_portal_blue"),
                Corner.BottomRight => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateReversed/smalltrapezeplatelinereversed_portal_blue"),
                Corner.None => null,
                _ => throw new KragException("Unknown Form of the cell.")
            };
            
            InitSmall(corner, _cellSize);
        }

        protected override void LoadSquareTexture()
        {
            _effectSprite.Texture =
                Engine.Instance.TextureCache.GetOrCache($"squareplate/squareplateline_portal_blue");
            
            InitSquare(_cellSize);
        }
    }
}