using KragmortaApp.Entities;
using SFML.Graphics;

namespace KragmortaApp.Drawables
{
    public class PortalCellDrawable : AbstractCellDrawable
    {
        public PortalCellDrawable(AbstractCell cell, int cellSize) : base(cell, cellSize,
            GameState.Instance.ColorsStorage.PortalCell)
        {
        }
    }
}