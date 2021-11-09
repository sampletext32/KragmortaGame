using System;
using MainApp.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.OnCreate();

            var scene = new GameTableScene();
            scene.OnCreate();
            game.SetActiveScene(scene);

            var videoMode = new VideoMode(
                width: 1280,
                height: 720,
                bpp: 24
            );

            var window = new RenderWindow(videoMode, "Kragmorta Game");

            window.Resized += (sender, eventArgs) => { window.SetView(new View(new FloatRect(0, 0, window.Size.X, window.Size.Y))); };

            window.Closed += (sender, eventArgs) =>
            {
                window.Close();
            };
            
            Clock clock = new Clock();

            while (window.IsOpen)
            {
                window.DispatchEvents();

                float deltaTime = clock.Restart().AsSeconds();
                game.OnUpdate(deltaTime);

                game.OnRender(window);

                window.Display();
            }
            
            Console.WriteLine("Exiting");
        }
    }
}