using System;
using Inventory.Items;

namespace SaveSystem.Main
{
    [Serializable]
    public class EquipSlotData
    {
        public string identrifier;
        public Item item;

        public EquipSlotData(string identrifier, Item item)
        {
            this.identrifier = identrifier;
            this.item = item;
        }
    }
}