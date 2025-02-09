using Equipment;
using Inventory.Items;
using Inventory.Main;

namespace Inventory.Services
{
    public class InventoryService : IInventoryService
    {
        private InventoryController _inventoryController;
        private EquipmentController _equipmentController;

        public void SetControllers(InventoryController inventoryController, EquipmentController equipmentController)
        {
            _inventoryController = inventoryController;
            _equipmentController = equipmentController;
        }

        public void AddItem(Item item)
        {
            _inventoryController?.AddItem(item);
        }

        public void RemoveItem(Item item)
        {
            _inventoryController?.RemoveItem(item);
        }

        public void EquipItem(Item item)
        {
            _equipmentController?.Equip(item);
        }

        public void LoadData(Item[] items)
        {
            _inventoryController?.LoadData(items);
        }

        public Item[] GetInventoryData()
        {
            return _inventoryController.GetInventoryData();
        }
    }
}