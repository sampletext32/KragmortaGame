using System.Collections.Generic;
using KragmortaApp.Entities.Cells;

namespace KragmortaApp.Entities
{
    public class Workbench : VisualEntity
    {
        public IReadOnlyList<WorkbenchCell> Cells => _cells;

        private List<WorkbenchCell> _cells;
        private GameField _field;

        public Workbench(GameField field)
        {
            _field = field;
            _cells      = new List<WorkbenchCell>(4);

        }
    }
}