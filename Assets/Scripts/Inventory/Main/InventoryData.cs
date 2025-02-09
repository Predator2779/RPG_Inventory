using System;
using Inventory.Items;
using UnityEngine;

namespace Inventory.Main
{
    [Serializable]
    public class InventoryData
    {
        public Action<Item[]> OnDataChanged;
        
        [field: SerializeField] public Item[] Items { get; set; }

        public InventoryData(Item[] items)
        {
            Items = items;
            OnDataChanged += items => { Items = items; };
        }
    }
}