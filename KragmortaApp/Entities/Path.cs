using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Entities.Cells;
using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities
{
    public class Path : VisualEntity
    {
        public IReadOnlyList<PathCell> Cells => _cells;
        private List<PathCell> _cells;

        public Path()
        {
            _cells = new List<PathCell>(4);
            _cells.Add(new PathCell { Visible = false });
            _cells.Add(new PathCell { Visible = false });
            _cells.Add(new PathCell { Visible = false });
            _cells.Add(new PathCell { Visible = false });
        }

        public Path(PathFileData fileData)
        {
            _cells = fileData.Cells.Select(f => new PathCell(f)).ToList();
        }

        public PathFileData ToFileData()
        {
            return new PathFileData()
            {
                Cells = _cells.Select(c => c.ToFileData()).ToList()
            };
        }
    }
}