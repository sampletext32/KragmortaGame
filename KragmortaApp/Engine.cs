using System.Collections.Generic;
using KragmortaApp.Scenes;
using SFML.Graphics;

namespace KragmortaApp
{
    public class Engine
    {
        public const int StartWindowWidth = 1280;
        public const int StartWindowHeight = 720;

        private static Engine _instance;
        public static Engine Instance => _instance;

        private Scene _activeScene;

        public TextureCache TextureCache => _textureCache;
        public ImageCache ImageCache => _imageCache;

        private TextureCache _textureCache;
        private ImageCache _imageCache;

        private int _windowWidth;
        private int _windowHeight;

        public int WindowWidth => _windowWidth;
        public int WindowHeight => _windowHeight;

        public RenderWindow Window { get; set; }

        /// <summary>
        /// Game constructor, should not initialize any entities
        /// </summary>
        public Engine()
        {
            if (_instance is not null)
            {
                throw new KragException("You can't create more than one game in the app!");
            }

            _instance     = this;
            _textureCache = new TextureCache();
            _imageCache   = new ImageCache();
            _windowWidth  = StartWindowWidth;
            _windowHeight = StartWindowHeight;
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

        public void OnWindowResized(int width, int height)
        {
            _windowWidth  = width;
            _windowHeight = height;
            _activeScene?.OnWindowResized(width, height);
        }

        public void OnMouseScrolled(int x, int y, bool isVertical, float delta)
        {
            _activeScene?.OnMouseScrolled(x, y, isVertical, delta);
        }
    }
}