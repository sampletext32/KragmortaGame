namespace KragmortaApp.Handlers
{
    public class PortalHandler : AbstractHandler
    {
        public void OnPortalCellClicked(int x, int y, KragMouseButton mouseButton)
        {
            // TODO:
            // Step on the portal
            //      If there is another player, push him out of this cell
            //      Do not keep the chain of pushing. Immediately jump into the portal
            // Highlight other portals except the one where the player stands right now.
            // (*) Player clicks on any highlighted portal.
            // He teleports. (Set his coords to the coords of the portal)
            //      If the cell, where the current player is teleporting, is reserved by another player, current
            //      player pushes that player. Therefore, the chain of pushing starts.
            //          In case during the chain of pushing a player steps on a portal, look at the very first
            //          point again.
            
            
            // This method activates from (*) point of the plan above.
        }
    }
}