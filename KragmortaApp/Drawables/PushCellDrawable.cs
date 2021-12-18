using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class PushCellDrawable : AbstractCellDrawable
    {
        public PushCellDrawable(PushCell pushCell, int cellSize) : base(pushCell, cellSize,
            GameState.Instance.ColorsStorage.PushCell)
        {
        }
    }
}