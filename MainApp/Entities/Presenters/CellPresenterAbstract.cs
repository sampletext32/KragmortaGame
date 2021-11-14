namespace MainApp.Entities.Presenters
{
    public abstract class CellPresenterAbstract : Presenter
    {
        public static readonly int CellSize = 96;
        public static readonly int CellMargin = 6;

        /// <summary>
        /// Converts the screen X to field Cell X
        /// </summary>
        public int ConvertMouseXToCellX(int x)
        {
            return x / (CellSize + CellMargin);
        }
        
        /// <summary>
        /// Converts the screen Y to field Cell Y
        /// </summary>
        public int ConvertMouseYToCellY(int y)
        {
            return y / (CellSize + CellMargin);
        }
    }
}