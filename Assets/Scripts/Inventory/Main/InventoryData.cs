using System;
using Inventory.Items;
using UnityEngine;

namespace Inventory.Main
{
    [Serializable]
    public class InventoryData
    {
        public Action<ItemData[]> OnDataChanged;
        
        [field: SerializeField] public ItemData[] Items { get; set; }

        public InventoryData()
        {
            Items = Array.Empty<ItemData>();
            OnDataChanged += items =>
            {
                Items = items;
                Debug.Log("Inventory Data changed");
            };
        }
    }
}