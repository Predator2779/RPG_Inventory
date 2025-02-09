using System.Collections.Generic;
using Inventory.Items;
using Inventory.Services;
using UnityEngine;

namespace Equipment
{
    public class EquipmentController
    {
        private readonly List<EquipSlot> _equipSlots;
        private readonly InventoryService _inventoryService;

        public EquipmentController(List<EquipSlot> equipSlots, InventoryService inventoryService)
        {
            _equipSlots = equipSlots;
            _inventoryService = inventoryService;
        }

        public void Equip(ItemData itemData)
        {
            var slot = _equipSlots.Find(s => s.SlotType == itemData.Type);
            if (slot == null)
            {
                Debug.LogError($"Not found slot for {itemData.Type}!");
                return;
            }

            ItemData previousItemData = slot.Unequip();
            slot.Equip(itemData);
            _inventoryService.RemoveItem(itemData.Index);

            if (previousItemData != null)
            {
                Debug.Log($"Item {previousItemData.Name} has been replaced by item {itemData.Name}");
                _inventoryService.AddItem(previousItemData);
            }
        }
    }
}