using System;
using System.Collections.Generic;
using Inventory.Items;

namespace SaveSystem.Main
{
    [Serializable]
    public class SaveData
    {
        public List<Item> inventoryItems = new();
        public List<KeyValuePair<string, Item>> equipedItems = new();
    }
}