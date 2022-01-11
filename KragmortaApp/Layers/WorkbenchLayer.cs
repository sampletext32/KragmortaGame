using KragmortaApp.Handlers;
using KragmortaApp.Presenters;

namespace KragmortaApp.Layers
{
    public class WorkbenchLayer : AbstractLayer
    {
        public WorkbenchLayer(Presenter presenter, WorkbenchHandler handler, string title = "Workbench layer") : base(presenter, handler, title)
        {
        }
    }
}