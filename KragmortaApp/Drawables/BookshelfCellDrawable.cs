using KragmortaApp.Entities;
using KragmortaApp.Entities.Cells;
using SFML.Graphics;

namespace KragmortaApp.Drawables
{
    public class BookshelfCellDrawable : AbstractCellDrawable
    {
        public BookshelfCellDrawable(AbstractCell cell, int cellSize, Color highlightedColor) : base(cell, cellSize, GameState.Instance.ColorsStorage.PathCell)
        {
        }
    }
}