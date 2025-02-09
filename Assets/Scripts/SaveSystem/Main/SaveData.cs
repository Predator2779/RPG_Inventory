using System;
using System.Collections.Generic;
using Inventory.Items;

namespace SaveSystem.Main
{
    [Serializable]
    public class SaveData
    {
        public List<ItemData> inventoryItems = new();
        public List<EquipSlotData> equipedItems = new();
    }
}