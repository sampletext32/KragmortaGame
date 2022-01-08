using System;
using System.Collections.Generic;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using SFML.Graphics;

namespace KragmortaApp.Presenters
{
    public class BookshelfPresenter : Presenter
    {
        private readonly Bookshelf _bookshelf;
        private List<BookshelfCellDrawable> _drawables;
        public static event Action<int, int> FieldOriginChanged;

        public BookshelfPresenter(Bookshelf bookshelf)
        {
            _bookshelf = bookshelf;

            _drawables = InitBookshelfCellDrawables();

            FieldOriginChanged += OnFieldOriginChanged;
        }

        private List<BookshelfCellDrawable> InitBookshelfCellDrawables()
        {
            var count = _bookshelf.Cells.Count;
            var result     = new List<BookshelfCellDrawable>(count);

            for (int i = 0; i < count; i++)
            {
                // result.Add(new BookshelfCellDrawable());
            }

            return null;
        }

        private void OnFieldOriginChanged(int arg1, int arg2)
        {
            
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return false;
        }

        public override void Render(RenderTarget target)
        {
            
        }
    }
}