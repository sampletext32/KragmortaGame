using System.Collections.Generic;

namespace MainApp.Scenes
{
    public class SceneStorage
    {
        private Dictionary<string, Scene> _loadedScenes;

        public SceneStorage()
        {
            _loadedScenes = new();
        }

        public void Add(string name, Scene scene)
        {
            _loadedScenes[name] = scene;
        }

        public bool TryGet(string name, out Scene scene)
        {
            if (_loadedScenes.ContainsKey(name))
            {
                scene = _loadedScenes[name];
                return true;
            }
            else
            {
                scene = null;
                return false;
            }
        }
    }
}