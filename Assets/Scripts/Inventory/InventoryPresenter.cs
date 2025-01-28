namespace Inventory
{
    public class InventoryPresenter
    {
        private const int TotalSlots = 30;
        
        public void Initialize(InventoryView view, InventoryData data)
        {
            view.CreateSlots(TotalSlots);
            view.onItemChanged += data.PlaceSlotInSlot;
            data.onItemListChanged += view.FillGrid;
            data.InitializeList();
        }
    }
}