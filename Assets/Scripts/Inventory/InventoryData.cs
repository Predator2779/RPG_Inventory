using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryData : MonoBehaviour
    {
        [SerializeField] private List<Item> _items = new List<Item>();

        public Action<Item[]> onItemListChanged;

        public void InitializeList()
        {
            onItemListChanged?.Invoke(_items.ToArray());
        }

        public void PlaceSlotInSlot(SlotView inSlot, SlotView outSlot)
        {
            var inputItem = inSlot.GetItem();
            var outputItem = outSlot.GetItem();

            if (outputItem == null)
            {
                inSlot.SetItem(outputItem);
                return;
            }
            
            if (inputItem.Type == outputItem.Type && outputItem.Stack + inputItem.Stack <= outputItem.MaxStack)
                outputItem.Stack += inputItem.Stack;
            else
            {
                inSlot.SetItem(outputItem);
                outSlot.SetItem(inputItem);
            }

            onItemListChanged?.Invoke(_items.ToArray());
        }
    }
}