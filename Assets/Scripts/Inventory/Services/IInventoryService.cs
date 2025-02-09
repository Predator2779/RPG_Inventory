using Inventory.Items;

namespace Inventory.Services
{
    public interface IInventoryService
    {
        public void AddItem(Item item);
        public void RemoveItem(Item item);
        public void BuyItem(int index);
        public void EquipItem(Item item);
    }
}