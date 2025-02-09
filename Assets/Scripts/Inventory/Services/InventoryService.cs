using Equipment;
using Inventory.Items;
using Inventory.Main;
using UnityEngine;

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

        public void AddItem(ItemData itemData)
        {
            _inventoryController?.AddItem(itemData);
        }

        public void RemoveItem(int index)
        {
            _inventoryController?.RemoveItem(index);
        }
        public void BuyItem(int index)
        {
            _inventoryController?.FillStack(index);
        }

        public bool TryGetItem(string name, int amount, out ItemData item)
        {
            return _inventoryController.TryGetItem(name, amount, out item);
        }
        
        public bool HasItem(string name, int amount)
        {
           return _inventoryController.HasItem(name, amount);
        }
        
        public void EquipItem(ItemData itemData)
        {
            _equipmentController?.Equip(itemData);
        }

        public void LoadData(ItemData[] items)
        {
            _inventoryController?.LoadData(items);
        }

        public void DrawItems()
        {
            Debug.Log("Drawed");
            _inventoryController?.DrawItems();
        }
        
        public ItemData[] GetInventoryData()
        {
            return _inventoryController.GetInventoryData();
        }
    }
}