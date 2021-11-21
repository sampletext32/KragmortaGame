﻿namespace MainApp.Entities
{
    public abstract class VisualEntity
    {
        public bool Dirty;

        public void MarkDirty()
        {
            Dirty = true;
        }

        public void ClearDirty()
        {
            Dirty = false;
        }
    }
}