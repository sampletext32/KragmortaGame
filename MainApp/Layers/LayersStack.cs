using System.Collections.Generic;

namespace MainApp.Layers
{
    public class LayersStack
    {
        private List<Layer> _layers;
        private int _layersCount;

        public LayersStack(int layerCount)
        {
            _layersCount = layerCount;
            _layers     = new List<Layer>(_layersCount);
        }

        public void AddLayer(Layer layer)
        {
            _layers.Add(layer);
        }
    }
}