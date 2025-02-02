using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inventory
{
    public class InventoryView
    {
        public Action<int, int> OnDragItem;
        
        private readonly SlotView _slotViewPrefab;
        private readonly Transform _parent;
        
        private SlotView[] _slots;

        public InventoryView(Transform parent, SlotView slotViewPrefab)
        {
            _slotViewPrefab = slotViewPrefab;
            _parent = parent;
        }

        public void CreateSlots(int amountSlots)
        {
            _slots = new SlotView[amountSlots];
            
            for(int i = 0; i < amountSlots; i++)
            {
                _slots[i] = Object.Instantiate(_slotViewPrefab, _parent);
                _slots[i].Index = i;
                _slots[i].OnDragItem += (x, y) => OnDragItem?.Invoke(x, y);
            }
        }
        
        public void FillGrid(Item[] items)
        {
            var itemsLength = items.Length;
            var slotsLength = _slots.Length;
            
            if (itemsLength > slotsLength)
            {
                Debug.LogWarning("The length of the array of items is greater than length of the array of slots!");
                return;
            }
            
            Clear();
            
            for(int i = 0; i < itemsLength; i++)
            {
                var item = items[i];
                var index = item.Index;
                
                if (index < slotsLength)
                {
                    _slots[index].SetItem(item);
                    continue;
                }
                
                Debug.LogWarning($"Item index out of range! Item: {item.Name}, index: {item.Index}");
            }
        }

        private void Clear()
        {
            foreach (var slot in _slots)
                slot.Clear();
        }
    }
}