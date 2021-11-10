namespace MainApp.Entities
{
    public class FieldCell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Hovered { get; set; }
        public FieldType Type { get; set; }

        public bool Selected { get; set; }

        public FieldCell()
        {
        }
    }
}