using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventoryData
    {
        [field: SerializeField] public Item[] Items { get; set; }

        public InventoryData(Item[] items) => Items = items;
    }
}