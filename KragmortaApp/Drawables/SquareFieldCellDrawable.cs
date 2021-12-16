using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class SquareFieldCellDrawable : FieldCellDrawable
    {
        public SquareFieldCellDrawable(FieldCell cell, int size) : base(cell)
        {
            _backgroundRectangle.SetPointCount(4);
            
            _backgroundRectangle.SetPoint(0, new Vector2f(0, 0));
            _backgroundRectangle.SetPoint(1, new Vector2f(size , 0));
            _backgroundRectangle.SetPoint(2, new Vector2f(size, size));
            _backgroundRectangle.SetPoint(3, new Vector2f(0, size));
            
            _backgroundRectangle.FillColor = Color.Red;

            _backgroundRectangle.Position = new Vector2f(40, 240);
        }
    }
}