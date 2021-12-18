using System;

namespace KragmortaApp.Presenters
{
    public abstract class CellPresenterAbstract : Presenter
    {
        public static readonly int CellSize = 96;
        public static readonly int CellMargin = 6;

        public static int FieldOriginX = 350;
        public static int FieldOriginY = 50;

        public static event Action<int, int> FieldOriginChanged;

        public static void InvokeFieldOriginChanged(int x, int y)
        {
            FieldOriginChanged?.Invoke(x, y);
        }

        /// <summary>
        /// Converts the screen X to field Cell X
        /// </summary>
        public int ConvertMouseXToCellX(int x)
        {
            return (x - FieldOriginX) / (CellSize + CellMargin);
        }

        /// <summary>
        /// Converts the screen Y to field Cell Y
        /// </summary>
        public int ConvertMouseYToCellY(int y)
        {
            return (y - FieldOriginY) / (CellSize + CellMargin);
        }
    }
}