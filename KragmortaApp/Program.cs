using System;
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

            var scene = new GameTableScene();
            scene.OnCreate();
            engine.SetActiveScene(scene);

            var videoMode = new VideoMode(
                width: Engine.StartWindowWidth,
                height: Engine.StartWindowHeight,
                bpp: 24
            );

            var window = new RenderWindow(videoMode, "Kragmorta Game");

            window.Resized += (sender, eventArgs) =>
            {
                window.SetView(new View(new FloatRect(0, 0, window.Size.X, window.Size.Y)));
                engine.OnWindowResized((int)eventArgs.Width, (int)eventArgs.Height);
            };

            window.Closed += (sender, eventArgs) => { window.Close(); };

            window.MouseMoved += (sender, eventArgs) => { engine.OnMouseMoved(eventArgs.X, eventArgs.Y); };

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