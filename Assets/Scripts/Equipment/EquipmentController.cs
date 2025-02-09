using System.Collections.Generic;
using Inventory;
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

        public void Equip(Item item)
        {
            var slot = _equipSlots.Find(s => s.SlotType == item.Type);
            if (slot == null)
            {
                Debug.LogError($"Not found slot for {item.Type}!");
                return;
            }

            Item previousItem = slot.Unequip();
            slot.Equip(item);
            _inventoryService.RemoveItem(item.Index);

            if (previousItem != null)
            {
                Debug.Log($"Item {previousItem.Name} has been replaced by item {item.Name}");
                _inventoryService.AddItem(previousItem);
            }
        }
    }
}