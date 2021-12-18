using System.Linq;

namespace KragmortaApp.UI
{
    public class VerticalLayout : UILayout
    {
        public VerticalLayout(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }

        protected override void Reflow()
        {
            var maxWidth  = Elements.Max(e => e.Width);
            var totalHeight = Elements.Sum(e => e.Height);

            int originX = X + Width / 2 - maxWidth / 2;
            int originY = Y + Height / 2 - totalHeight / 2;

            for (var i = 0; i < Elements.Count; i++)
            {
                Elements[i].X = originX;
                Elements[i].Y = originY;

                originY += Elements[i].Height + 10;
                Elements[i].ApplyReflow();
            }
        }
    }
}