using KragmortaApp.Entities;
using KragmortaApp.Enums;

namespace KragmortaApp.Drawables
{
    public class PushCellDrawable : AbstractCellDrawable
    {
        private readonly int _cellSize;

        public PushCellDrawable(PushCell pushCell, int cellSize) : base(pushCell, cellSize,
            GameState.Instance.ColorsStorage.PushCell)
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
                    $"bigtrapezeplate/BigTrapezePlate/bigtrapezeplateline_push"),
                Corner.TopRight => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateRef/bigtrapezeplatelineref_push"),
                Corner.BottomLeft => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateRefReversed/bigtrapezeplatelinerefreversed_push"),
                Corner.BottomRight => Engine.Instance.TextureCache.GetOrCache(
                    $"bigtrapezeplate/BigTrapezePlateReversed/bigtrapezeplatelinereversed_push"),
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
                    $"smalltrapezeplate/SmallTrapezePlate/smalltrapezeplateline_push"),
                Corner.TopRight => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateRef/smalltrapezeplatelineref_push"),
                Corner.BottomLeft => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateRefReversed/smalltrapezeplatelinerefreversed_push"),
                Corner.BottomRight => Engine.Instance.TextureCache.GetOrCache(
                    $"smalltrapezeplate/SmallTrapezePlateReversed/smalltrapezeplatelinereversed_push"),
                Corner.None => null,
                _ => throw new KragException("Unknown Form of the cell.")
            };
            
            InitSmall(corner, _cellSize);
        }

        protected override void LoadSquareTexture()
        {
            _effectSprite.Texture =
                Engine.Instance.TextureCache.GetOrCache($"squareplate/squareplateline_push");
            
            InitSquare(_cellSize);
        }
    }
}