using System.Collections.Generic;
using System.Linq;
using Inventory.Items;
using PopupManagement.Factory;
using PopupManagement.Popups;
using UnityEngine;

namespace Inventory.Main
{
    public class InventoryController
    {
        private readonly InventoryView _view;
        private readonly InventoryData _data;
        private readonly ItemPopup _itemPopup;
        private readonly PopupFactory _factory;
        private readonly int _totalSlots;
        
        private Dictionary<int, Item> _itemsDict;

        public InventoryController(InventoryView view, InventoryData data, ItemPopup itemPopup, int totalSlots)
        {
            _view = view;
            _data = data;
            _itemPopup = itemPopup;
            _totalSlots = totalSlots;
            _itemsDict = CreateDict(_data.Items.ToArray());
        }

        public void Initialize()
        {
            _view.OnDragItem += PlaceSlotInSlot;
            _view.OnItemUse += ShowItemPopup;
            _view.CreateSlots(_totalSlots);
            DrawItems();
        }

        public void LoadData(Item[] items)
        {
            ChangeDict(items);
            ChangeData();
            DrawItems();
        }

        public void AddItem(Item item)
        {
            foreach (var slotItem in _itemsDict)
            {
                if (slotItem.Value.Name == item.Name && slotItem.Value.Stack < slotItem.Value.MaxStack)
                {
                    int availableSpace = slotItem.Value.MaxStack - slotItem.Value.Stack;
                    int amountToAdd = Mathf.Min(item.Stack, availableSpace);

                    slotItem.Value.Stack += amountToAdd;
                    item.Stack -= amountToAdd;

                    Debug.Log($"Added {amountToAdd} к {slotItem.Value.Name}, now in stack: {slotItem.Value.Stack}");

                    if (item.Stack <= 0)
                    {
                        DrawItems();
                        return;
                    }
                }
            }
            
            int emptySlotIndex = _itemsDict.Count < _totalSlots ? _itemsDict.Count : -1;
            if (emptySlotIndex == -1)
            {
                Debug.LogWarning("There is no free space in the inventory!");
                return;
            }

            item.Index = emptySlotIndex;
            _itemsDict[emptySlotIndex] = item;

            ChangeData();
            DrawItems();
            
            Debug.Log($"Item {item.Name} added to new slot");
        }
        
        public void RemoveItem(Item item)
        {
            var slotIndex = _itemsDict.FirstOrDefault(x => x.Value == item).Key;

            if (!_itemsDict.ContainsKey(slotIndex))
            {
                Debug.LogWarning($"Item {item.Name} not found in inventory!");
                return;
            }

            if (item.Stack > 1)
            {
                item.Stack--;
                Debug.Log($"Removed one {item.Name}, remaining: {item.Stack}");
            }
            else
            {
                _itemsDict.Remove(slotIndex);
                Debug.Log($"Removed {item.Name} from inventory.");
            }

            ChangeData();
            DrawItems();
        }
        
        private void ShowItemPopup(Item item) => _itemPopup.Show(item);
        
        private void PlaceSlotInSlot(int inputSlotIndex, int outputSlotIndex)
        {
            HandleSlots(inputSlotIndex, outputSlotIndex);
            ChangeData();
            DrawItems();
        }

        private void HandleSlots(int inputSlotIndex, int outputSlotIndex)
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
            
            if (inputItem.Type != outputItem.Type
                || inputItem.Name != outputItem.Name
                || outputItem.Stack >= outputItem.MaxStack)
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

            if (arr.Length > 0)
                foreach (var item in arr)
                    dict[item.Index] = item;

            return dict;
        }

        private bool Has(int index) => _itemsDict.ContainsKey(index);
        private void DrawItems() => DrawItems(_data.Items);
        private void DrawItems(Item[] items) => _view.FillGrid(items);
        private void ChangeData() => ChangeData(GetItemsFromDict());
        private void ChangeData(Item[] items) => _data.OnDataChanged?.Invoke(items);
        private void ChangeDict(Item[] items) => _itemsDict = CreateDict(items);
        private Item[] GetItemsFromDict() => _itemsDict.Values.ToArray();
        public Item[] GetInventoryData() => _data.Items.ToArray();
        private void Remove(int index) => _itemsDict.Remove(index);

        private void Add(int index, Item item)
        {
            item.Index = index;
            _itemsDict.Add(index, item);
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            _itemsDict[firstIndex].Index = secondIndex;
            _itemsDict[secondIndex].Index = firstIndex;

            (_itemsDict[firstIndex], _itemsDict[secondIndex]) = (_itemsDict[secondIndex], _itemsDict[firstIndex]);
        }
    }
}