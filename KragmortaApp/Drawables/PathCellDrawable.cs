using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class PathCellDrawable : AbstractCellDrawable
    {
        public PathCellDrawable(PathCell pathCell, int cellSize) : base(pathCell, cellSize, GameState.Instance.ColorsStorage.PathCell)
        {
        }
    }
}