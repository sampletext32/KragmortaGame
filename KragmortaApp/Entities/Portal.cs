using System.Collections.Generic;

namespace KragmortaApp.Entities
{
    public class Portal : VisualEntity
    {
        private readonly GameField _field;
        public IReadOnlyList<AbstractCell> Cells => _cells;
        private List<AbstractCell> _cells;

        public Portal(GameField field)
        {
            // _field = field;
            // _cells = new List<AbstractCell>(7);
            //
            // if (field.SizeX == 10)
            // {
            //     Init10X7();
            // }
            // else
            // {
            //     Init7X10();
            // }
            //
            // foreach (var cell in _cells)
            // {
            //     cell.IsPortal = true;
            // }
        }

        private void Init7X10()
        {
            _cells.Add(_field.GetCell(0, 0));
            _cells.Add(_field.GetCell(6, 0));
            _cells.Add(_field.GetCell(3, 2));
            _cells.Add(_field.GetCell(1, 4));
            _cells.Add(_field.GetCell(5, 4));
            _cells.Add(_field.GetCell(4, 6));
            _cells.Add(_field.GetCell(6, 9));
        }

        private void Init10X7()
        {
        }
    }
}