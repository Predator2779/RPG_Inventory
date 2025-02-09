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
        
        private Dictionary<int, ItemData> _itemsDict;

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

        public void LoadData(ItemData[] items)
        {
            ChangeDict(items);
            ChangeData();
            DrawItems();
        }

        public void AddItem(ItemData itemData)
        {
            foreach (var slotItem in _itemsDict)
            {
                if (slotItem.Value.Name == itemData.Name 
                    && slotItem.Value.Type == itemData.Type 
                    && slotItem.Value.Stack < slotItem.Value.MaxStack)
                {
                    int availableSpace = slotItem.Value.MaxStack - slotItem.Value.Stack;
                    int amountToAdd = Mathf.Min(itemData.Stack, availableSpace);

                    slotItem.Value.Stack += amountToAdd;
                    itemData.Stack -= amountToAdd;

                    Debug.Log($"Added {amountToAdd} к {slotItem.Value.Name}, now in stack: {slotItem.Value.Stack}");

                    if (itemData.Stack <= 0)
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

            itemData.Index = emptySlotIndex;
            _itemsDict[emptySlotIndex] = itemData;

            ChangeData();
            DrawItems();
            
            Debug.Log($"Item {itemData.Name} added to new slot");
        }
        
        public void RemoveItem(int index)
        {
            if (!_itemsDict.ContainsKey(index))
            {
                Debug.LogWarning($"Item with index {index} not found in inventory!");
                return;
            }

            _itemsDict.Remove(index);
            Debug.Log($"Remove item with index {index} from inventory.");

            ChangeData();
            DrawItems();
        }

        public void FillStack(int index)
        {
            if (_itemsDict.TryGetValue(index, out var item))
            {
                item.Stack = item.MaxStack;
                Debug.Log($"{item.Name} filled.");
            }
            else
            {
                Debug.LogWarning($"Item {item.Name} not found in inventory!");
            }

            ChangeData();
            DrawItems();
        }

        public bool TryGetItem(string name, int amount, out ItemData item)
        {
            item = null;
            var arr = GetItemsFromDict();
            foreach (var data in arr)
            {
                if (data.Name == name && data.Stack >= amount)
                {
                    item = data;
                    return true;
                }
            }

            return false;
        }
        
        public bool HasItem(string name, int amount)
        {
            var arr = GetItemsFromDict();
            return arr.Any(item => item.Name == name && item.Stack >= amount);
        }
        
        private void ShowItemPopup(ItemData itemData) => _itemPopup.Show(itemData);
        
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

        private Dictionary<int, ItemData> CreateDict(ItemData[] items)
        {
            var arr = items;
            var dict = new Dictionary<int, ItemData>();

            if (arr.Length > 0)
                foreach (var item in arr)
                    dict[item.Index] = item;

            return dict;
        }

        public void DrawItems() => DrawItems(_data.Items);
        private void DrawItems(ItemData[] items) => _view.FillGrid(items);
        private bool Has(int index) => _itemsDict.ContainsKey(index);
        private void ChangeData() => ChangeData(GetItemsFromDict());
        private void ChangeData(ItemData[] items) => _data.OnDataChanged?.Invoke(items);
        private void ChangeDict(ItemData[] items) => _itemsDict = CreateDict(items);
        private ItemData[] GetItemsFromDict() => _itemsDict.Values.ToArray();
        public ItemData[] GetInventoryData() => _data.Items.ToArray();
        private void Remove(int index) => _itemsDict.Remove(index);

        private void Add(int index, ItemData itemData)
        {
            itemData.Index = index;
            _itemsDict.Add(index, itemData);
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            _itemsDict[firstIndex].Index = secondIndex;
            _itemsDict[secondIndex].Index = firstIndex;

            (_itemsDict[firstIndex], _itemsDict[secondIndex]) = (_itemsDict[secondIndex], _itemsDict[firstIndex]);
        }
    }
}