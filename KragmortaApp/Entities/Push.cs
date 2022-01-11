using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Entities.Cells;
using KragmortaApp.FileDatas;

namespace KragmortaApp.Entities
{
    public class Push : VisualEntity
    {
        public IReadOnlyList<PushCell> Cells => _cells;
        private List<PushCell> _cells;

        public Push()
        {
            _cells = new List<PushCell>(4);
            _cells.Add(new PushCell { Visible = false });
            _cells.Add(new PushCell { Visible = false });
            _cells.Add(new PushCell { Visible = false });
            _cells.Add(new PushCell { Visible = false });
        }

        public Push(PushFileData fileData)
        {
            _cells = fileData.Cells.Select(f => new PushCell(f)).ToList();
        }

        public PushFileData ToFileData()
        {
            return new PushFileData()
            {
                Cells = _cells.Select(c => c.ToFileData()).ToList()
            };
        }
    }
}