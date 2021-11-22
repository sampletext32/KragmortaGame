using System.Collections.Generic;

namespace MainApp.Entities
{
    public class Path : VisualEntity
    {
        public IReadOnlyList<PathCell> Cells => _cells;
        private List<PathCell> _cells;

        public Path()
        {
            _cells = new List<PathCell>(4);
            _cells.Add(new PathCell() { Visible = false });
            _cells.Add(new PathCell() { Visible = false });
            _cells.Add(new PathCell() { Visible = false });
            _cells.Add(new PathCell() { Visible = false });
        }
    }
}