namespace KragmortaApp.Entities
{
    public class RigorModel : VisualEntity
    {
        public int FieldX { get; private set; }

        public int FieldY { get; private set; }


        public RigorModel()
        {
            FieldX   = 3;
            FieldY   = 9;
        }

        public void SetFieldPosition(int x, int y)
        {
            FieldX = x;
            FieldY = y;
            Dirty  = true;
        }
    }
}