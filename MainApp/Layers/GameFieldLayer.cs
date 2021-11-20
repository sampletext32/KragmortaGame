using MainApp.Handlers;
using MainApp.Presenters;

namespace MainApp.Layers
{
    public class GameFieldLayer : Layer
    {
        public GameFieldLayer(GameFieldPresenter presenter, GameFieldHandler handler, string title = "GameField Layer") : base(presenter, handler, title)
        {
        }
    }
}