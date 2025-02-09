using Inventory.Items;

namespace Inventory
{
    public interface IInventoryService
    {
        public void AddItem(Item item);
        public void RemoveItem(Item item);
        public void EquipItem(Item item);
    }
}