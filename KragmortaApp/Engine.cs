using System.Collections.Generic;
using System.Diagnostics;
using KragmortaApp.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace KragmortaApp
{
    public class Engine
    {
        private static Engine _instance;
        public static Engine Instance => _instance;

        private Scene _activeScene;

        private Stack<Scene> _scenesStack;

        public TextureCache TextureCache => _textureCache;
        public ImageCache ImageCache => _imageCache;
        public FontCache FontCache => _fontCache;
        public SoundCache SoundCache => _soundCache;

        private TextureCache _textureCache;
        private ImageCache _imageCache;
        private FontCache _fontCache;
        private SoundCache _soundCache;

        private int _windowWidth;
        private int _windowHeight;

        public int WindowWidth => _windowWidth;
        public int WindowHeight => _windowHeight;

        public RenderWindow Window { get; set; }

        public SettingsData Settings { get; set; }

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
            _fontCache   = new FontCache();
            _soundCache   = new SoundCache();
            
            _scenesStack  = new Stack<Scene>();

            Settings = SettingsData.Load();
        }

        public void SetWindowSize(int width, int height)
        {
            _windowWidth    = width;
            _windowHeight   = height;
            Window.Size     = new Vector2u((uint)_windowWidth, (uint)_windowHeight);
            Window.Position = new Vector2i((int)(VideoMode.DesktopMode.Width / 2 - width / 2), (int)(VideoMode.DesktopMode.Height / 2 - height / 2));
            Window.SetView(new View(new FloatRect(0, 0, width, height)));
            OnWindowResized(width, height);
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
        public void PushScene(Scene scene)
        {
            if (!scene.OnCreateCalled)
            {
                scene.OnCreate();
            }
            _scenesStack.Push(_activeScene);
            _activeScene = scene;
        }

        public void PopScene()
        {
            if (_scenesStack.Count > 0)
            {
                _activeScene = _scenesStack.Pop();
            }
        }

        public void Close()
        {
            Process.GetCurrentProcess().Kill();
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
            _soundCache.GetOrCache("click").Play();
        }

        public void OnWindowResized(int width, int height)
        {
            _windowWidth  = width;
            _windowHeight = height;
            foreach (var scene in _scenesStack)
            {
                scene?.OnWindowResized(width, height);
            }

            _activeScene?.OnWindowResized(width, height);
        }

        public void OnMouseScrolled(int x, int y, bool isVertical, float delta)
        {
            _activeScene?.OnMouseScrolled(x, y, isVertical, delta);
        }

        public void OnKeyPressed(Keyboard.Key code)
        {
            _activeScene?.OnKeyPressed(code);
        }
    }
}