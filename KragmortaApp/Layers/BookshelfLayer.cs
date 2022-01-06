using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class BookshelfLayer : AbstractLayer
    {
        public BookshelfLayer(BookshelfPresenter presenter, BookshelfHandler handler, string title = "Bookshelf layer") : base(presenter, handler, title)
        {
        }
    }
}