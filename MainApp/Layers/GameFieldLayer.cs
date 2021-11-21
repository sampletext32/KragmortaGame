using MainApp.Handlers;
using MainApp.Presenters;

namespace MainApp.Layers
{
    public class GameFieldLayer : AbstractLayer
    {
        public GameFieldLayer(GameFieldPresenter presenter, GameFieldHandler handler, string title = "GameField Layer") : base(presenter, handler, title)
        {
        }

        public override bool TryHandleMouseMoved(int x, int y)
        {
            
            return base.TryHandleMouseMoved(x, y);
        }
    }
}