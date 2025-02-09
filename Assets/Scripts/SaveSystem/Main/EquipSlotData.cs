using System;
using Inventory.Items;
using UnityEngine.Serialization;

namespace SaveSystem.Main
{
    [Serializable]
    public class EquipSlotData
    {
        public string identrifier;
        public ItemData itemData;

        public EquipSlotData(string identrifier, ItemData itemData)
        {
            this.identrifier = identrifier;
            this.itemData = itemData;
        }
    }
}