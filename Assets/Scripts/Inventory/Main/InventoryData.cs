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

        public InventoryData()
        {
            Items = Array.Empty<Item>();
            OnDataChanged += items =>
            {
                Items = items;
                Debug.Log("Inventory Data changed");
            };
        }
    }
}