using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SfmlTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var renderWindow = new RenderWindow(new VideoMode(1280, 720), "Tester");
            renderWindow.Closed += (_, _) => { renderWindow.Close(); };

            var shape = new RectangleShape(new Vector2f(100, 100))
            {
                FillColor = Color.Green
            };

            Sprite sprite = new Sprite();
            

            while (renderWindow.IsOpen)
            {
                renderWindow.DispatchEvents();
                renderWindow.Clear(Color.White);
                renderWindow.Draw(shape);
                renderWindow.Display();
            }
        }
    }
}