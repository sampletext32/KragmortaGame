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
            SceneStorage sceneStorage = new SceneStorage();
            Game game = new Game();
            game.OnCreate();

            var videoMode = new VideoMode(
                width: 1280,
                height: 720,
                bpp: 24
            );

            var window = new RenderWindow(videoMode, "Kragmorta Game");

            Clock clock = new Clock();

            while (window.IsOpen)
            {
                window.DispatchEvents();

                float deltaTime = clock.Restart().AsSeconds();
                game.OnUpdate(deltaTime);

                game.OnRender(window);

                window.Display();
            }
        }
    }
}