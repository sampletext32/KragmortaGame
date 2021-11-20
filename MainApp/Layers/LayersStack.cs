using System.Collections.Generic;
using SFML.Graphics;

namespace MainApp.Layers
{
    public class LayersStack
    {
        private Font _font;
        private Text _layerDebugText;

        private List<Layer> _layers;
        private int _layersCount;

        private Layer _lastMouseOverLayer;
        private Layer _lastMousePressedLayer;

        public LayersStack(int layerCount)
        {
            _layersCount = layerCount;
            _layers      = new List<Layer>(_layersCount);

            _font                         = new Font("assets/fonts/arial.ttf");
            _layerDebugText               = new Text();
            _layerDebugText.Font          = _font;
            _layerDebugText.CharacterSize = 16;
            _layerDebugText.FillColor     = Color.Red;
        }

        public void Render(RenderTarget target)
        {
            for (var i = 0; i < _layers.Count; i++)
            {
                _layers[i].Render(target);
            }

            target.Draw(_layerDebugText);
        }

        public void AddLayer(Layer layer)
        {
            _layers.Add(layer);
            _layerDebugText.DisplayedString += layer.Title + "\n";
        }

        public void OnMousePressed(int x, int y, KragMouseButton mouseButton)
        {
            for (var i = _layers.Count - 1; i >= 0; i--)
            {
                if (_layers[i].TryHandleMousePressed(x, y, mouseButton))
                {
                    _lastMousePressedLayer = _layers[i];
                    // If a layer has intercepted the event, then it was handled
                    break;
                }
            }
        }

        public void OnMouseReleased(int x, int y, KragMouseButton mouseButton)
        {
            _lastMousePressedLayer?.HandleMouseReleased(x, y, mouseButton);
        }

        public void OnMouseMoved(int x, int y)
        {
            Layer mouseOverLayer = null;
            for (var i = _layers.Count - 1; i >= 0; i--)
            {
                if (_layers[i].TryHandleMouseMoved(x, y))
                {
                    mouseOverLayer = _layers[i];
                    // If a layer has intercepted the event, then it was handled
                    break;
                }
            }

            if (_lastMouseOverLayer != mouseOverLayer)
            {
                _lastMouseOverLayer?.HandleMouseLeft();
            }

            _lastMouseOverLayer = mouseOverLayer;
        }

        public void OnWindowResized(int width, int height)
        {
            for (var i = _layers.Count - 1; i >= 0; i--)
            {
                _layers[i].HandleWindowResized(width, height);
            }
        }
    }
}