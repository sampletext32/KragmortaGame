using KragmortaApp.Entities;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class BigPolygonFieldCellDrawable : FieldCellDrawable
    {
        public BigPolygonFieldCellDrawable(FieldCell cell, int size) : base(cell)
        {
            _backgroundRectangle.SetPointCount(5);

            _backgroundRectangle.SetPoint(0, new Vector2f(0, 0));
            _backgroundRectangle.SetPoint(1, new Vector2f(size * 0.7f, 0));
            _backgroundRectangle.SetPoint(2, new Vector2f(size, size * 0.5f));
            _backgroundRectangle.SetPoint(3, new Vector2f(size * 0.6f, size));
            _backgroundRectangle.SetPoint(4, new Vector2f(0, size * 0.7f));

            _backgroundRectangle.FillColor = Color.Red;
            
            _backgroundRectangle.Position = new Vector2f(40, 240);

        }
    }
}