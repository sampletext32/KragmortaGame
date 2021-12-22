using KragmortaApp.Entities;

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