using System;
using SFML.Window;

namespace MainApp
{
    public enum KragMouseButton
    {
        Left,
        Right
    }

    public static class MouseButtonExtensions
    {
        public static KragMouseButton ToKragMouseButton(this Mouse.Button button)
        {
            switch (button)
            {
                case Mouse.Button.Left:
                    return KragMouseButton.Left;
                case Mouse.Button.Right:
                    return KragMouseButton.Right;
                case Mouse.Button.Middle:
                case Mouse.Button.XButton1:
                case Mouse.Button.XButton2:
                case Mouse.Button.ButtonCount:
                default:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Mouse Button {button} is not supported now. Defaulted to Left");
                    Console.ForegroundColor = ConsoleColor.Black;
                    return KragMouseButton.Left;
                }
            }
        }
    }
}