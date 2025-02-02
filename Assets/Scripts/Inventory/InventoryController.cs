using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class InventoryController
    {
        private readonly InventoryView _view;
        private readonly InventoryData _data;
        private readonly Dictionary<int, Item> _itemsDict;
        private readonly int _totalSlots;

        public InventoryController(InventoryView view, InventoryData data, int totalSlots)
        {
            _view = view;
            _data = data;
            _totalSlots = totalSlots;
            _itemsDict = CreateDict(_data.Items);
        }

        public void Initialize()
        {
            _view.OnDragItem += PlaceSlotInSlot;
            _view.CreateSlots(_totalSlots);
            DrawItems();
        }
        
        private void PlaceSlotInSlot(int inputSlotIndex, int outputSlotIndex)
        {
            Handle(inputSlotIndex, outputSlotIndex);
            _data.Items = GetItemsArray();
            DrawItems(_data.Items);
        }

        private void Handle(int inputSlotIndex, int outputSlotIndex)
        {
            if (!Has(inputSlotIndex))
            {
                Debug.LogWarning($"Inventory Data is not contains Input Slot: {inputSlotIndex}");
                return;
            }

            if (!Has(outputSlotIndex))
            {
                Add(outputSlotIndex, _itemsDict[inputSlotIndex]);
                Remove(inputSlotIndex);
                Debug.Log($"Added new item from: {inputSlotIndex}, to: {outputSlotIndex}");
                return;
            }

            var inputItem = _itemsDict[inputSlotIndex];
            var outputItem = _itemsDict[outputSlotIndex];

            if (inputItem.Type != outputItem.Type || outputItem.Stack >= outputItem.MaxStack)
            {
                Swap(inputSlotIndex, outputSlotIndex);
                Debug.Log($"The items have swapped places: [{inputSlotIndex}] <-> [{outputSlotIndex}]");
                return;
            }

            if (outputItem.Stack + inputItem.Stack <= outputItem.MaxStack)
            {
                outputItem.Stack += inputItem.Stack;
                Remove(inputSlotIndex);
                Debug.Log($"The item {inputSlotIndex} has been added to {outputSlotIndex}");
                return;
            }

            var remainder = outputItem.Stack + inputItem.Stack - outputItem.MaxStack;
            outputItem.Stack = outputItem.MaxStack;
            inputItem.Stack = remainder;

            Debug.Log($"The item {inputSlotIndex} has been added to {outputSlotIndex} without remainder");
        }

        private Dictionary<int, Item> CreateDict(Item[] items)
        {
            var arr = items;
            var dict = new Dictionary<int, Item>();

            foreach (var item in arr)
                dict[item.Index] = item;

            return dict;
        }

        private void DrawItems() => _view.FillGrid(GetItemsArray());
        private void DrawItems(Item[] items) => _view.FillGrid(items);
        private Item[] GetItemsArray() => _itemsDict.Values.ToArray();
        private bool Has(int index) => _itemsDict.ContainsKey(index);

        private void Add(int index, Item item)
        {
            item.Index = index;
            _itemsDict.Add(index, item);
        }
        
        private void Remove(int index) => _itemsDict.Remove(index);

        private void Swap(int firstIndex, int secondIndex)
        {
            _itemsDict[firstIndex].Index = secondIndex;
            _itemsDict[secondIndex].Index = firstIndex;

            (_itemsDict[firstIndex], _itemsDict[secondIndex]) = (_itemsDict[secondIndex], _itemsDict[firstIndex]);
        }
    }
}