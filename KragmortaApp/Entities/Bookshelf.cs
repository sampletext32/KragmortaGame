using System.Collections.Generic;
using System.Threading;
using KragmortaApp.Entities.Cells;
using KragmortaApp.Enums;

namespace KragmortaApp.Entities
{
    public class Bookshelf
    {
        public IReadOnlyList<BookshelfCell> Cells => _cells;
        private List<BookshelfCell> _cells;

        public Bookshelf(GameField field)
        {
            _cells = new List<BookshelfCell>();
            switch (field.FieldType)
            {
                case FieldType.Mini:
                {
                    var flipper = 0;
                    foreach (var cell in field.Cells)
                    {
                        if (cell.Type == CellType.Wall)
                        {
                            if (flipper % 2 == 0)
                            {
                                _cells.Add(new BookshelfCell()
                                {
                                    Head = cell
                                });
                            }
                            else
                            {
                                _cells[^1].Tail = cell;
                            }
                            flipper++;
                        }
                    }

                    break;
                }
            }
        }
    }
}