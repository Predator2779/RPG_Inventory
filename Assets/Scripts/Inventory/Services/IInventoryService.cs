using Inventory.Items;

namespace Inventory.Services
{
    public interface IInventoryService
    {
        public void AddItem(ItemData itemData);
        public void RemoveItem(int index);
        public void BuyItem(int index);
        public void EquipItem(ItemData itemData);
        public void DrawItems();
    }
}