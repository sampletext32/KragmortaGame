using System;
using System.Threading.Channels;
using KragmortaApp.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace KragmortaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.OnCreate();

            var scene = new MenuScene();
            scene.OnCreate();
            engine.PushScene(scene);

            var videoMode = engine.Settings.FullScreen
                ? VideoMode.FullscreenModes[0]
                : new VideoMode(
                    width: 1280,
                    height: 720,
                    bpp: 24
                );

            var window = new RenderWindow(videoMode, "Kragmorta Game", engine.Settings.FullScreen ? Styles.Fullscreen : Styles.Close);

            engine.Window = window;

            window.Resized += (sender, eventArgs) =>
            {
                window.SetView(new View(new FloatRect(0, 0, window.Size.X, window.Size.Y)));
                engine.OnWindowResized((int)eventArgs.Width, (int)eventArgs.Height);
            };


            if (engine.Settings.FullScreen)
            {
                engine.SetWindowSize((int)videoMode.Width, (int)videoMode.Height);
            }
            else
            {
                engine.SetWindowSize(Engine.Instance.Settings.ResolutionWidth,
                    Engine.Instance.Settings.ResolutionHeight);
            }


            window.Closed += (sender, eventArgs) => { window.Close(); };

            window.MouseMoved += (sender, eventArgs) => { engine.OnMouseMoved(eventArgs.X, eventArgs.Y); };

            bool isShiftPressed = false;

            window.KeyPressed  += (sender, eventArgs) => { isShiftPressed |= eventArgs.Shift; };
            window.KeyReleased += (sender, eventArgs) => { isShiftPressed &= eventArgs.Shift; };

            window.MouseWheelScrolled += (sender, eventArgs) =>
            {
                if (eventArgs.Wheel == Mouse.Wheel.VerticalWheel)
                {
                    if (isShiftPressed)
                    {
                        engine.OnMouseScrolled(eventArgs.X, eventArgs.Y, false, eventArgs.Delta);
                    }
                    else
                    {
                        engine.OnMouseScrolled(eventArgs.X, eventArgs.Y, true, eventArgs.Delta);
                    }
                }
                else
                {
                    engine.OnMouseScrolled(eventArgs.X, eventArgs.Y, false, eventArgs.Delta);
                }
            };

            window.MouseButtonPressed  += (sender, eventArgs) => { engine.OnMouseButtonPressed(eventArgs.X, eventArgs.Y, eventArgs.Button.ToKragMouseButton()); };
            window.MouseButtonReleased += (sender, eventArgs) => { engine.OnMouseButtonReleased(eventArgs.X, eventArgs.Y, eventArgs.Button.ToKragMouseButton()); };

            Clock clock = new Clock();

            window.SetFramerateLimit(60);

            while (window.IsOpen)
            {
                window.DispatchEvents();

                float deltaTime = clock.Restart().AsSeconds();
                engine.OnUpdate(deltaTime);

                window.Clear();
                engine.OnRender(window);

                window.Display();
            }

            Console.WriteLine("Exiting");
        }
    }
}