using System.Collections.Generic;

namespace KragmortaApp.Entities
{
    public class Push : VisualEntity
    {
        public IReadOnlyList<PushCell> Cells => _cells;
        private List<PushCell> _cells;

        public Push()
        {
            _cells = new List<PushCell>(4);
            _cells.Add(new PushCell() { Visible = false });
            _cells.Add(new PushCell() { Visible = false });
            _cells.Add(new PushCell() { Visible = false });
            _cells.Add(new PushCell() { Visible = false });
        }
    }
}