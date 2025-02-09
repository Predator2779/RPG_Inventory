using System;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace Equipment
{
    [Serializable]
    public class EquipmentSetting
    {
        [SerializeField] private List<EquipSlot> _equipSlots = new();

        public List<EquipSlot> EquipSlots => _equipSlots;
        
        public EquipmentController Initialize(InventoryService inventoryService)
        {
            return new EquipmentController(_equipSlots, inventoryService);
        }
    }
}