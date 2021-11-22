namespace MainApp.Entities
{
    public abstract class VisualEntity
    {
        public bool Dirty = true;

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