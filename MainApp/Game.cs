using System.Collections.Generic;
using MainApp.Scenes;
using SFML.Graphics;

namespace MainApp
{
    public class Game
    {
        private static Game _instance;
        public static Game Instance => _instance;

        private Scene _activeScene;

        /// <summary>
        /// Game constructor, should not initialize any entities
        /// </summary>
        public Game()
        {
            if (_instance is not null)
            {
                throw new KragException("You can't create more than one game in the app!");
            }

            _instance = this;
        }

        /// <summary>
        /// returns the currently active scene
        /// </summary>
        public Scene GetActiveScene()
        {
            return _activeScene;
        }

        /// <summary>
        /// sets the currently active scene
        /// </summary>
        public void SetActiveScene(Scene scene)
        {
            _activeScene = scene;
        }

        /// <summary>
        /// Initializes all entities
        /// </summary>
        public void OnCreate()
        {
        }

        /// <summary>
        /// This method is called every frame, so the game can update the states
        /// </summary>
        public void OnUpdate(float deltaTime)
        {
            _activeScene?.OnUpdate(deltaTime);
        }

        /// <summary>
        /// This method is called every frame, so the game can render itself
        /// </summary>
        public void OnRender(RenderTarget renderTarget)
        {
            _activeScene?.OnRender(renderTarget);
        }

        public void OnMouseMoved(int x, int y)
        {
            _activeScene?.OnMouseMoved(x, y);
        }

        public void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            _activeScene?.OnMouseButtonPressed(x, y, mouseButton);
        }

        public void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            _activeScene?.OnMouseButtonReleased(x, y, mouseButton);
        }
    }
}