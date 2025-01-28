using System;
using UnityEngine;

namespace Inventory
{
    public class InventoryView : MonoBehaviour
    {
        public Action<SlotView, SlotView> onItemChanged;
        
        [SerializeField] private Transform _inventoryGrid;
        [SerializeField] private SlotView _slotViewPrefab;

        private SlotView[] _slots;

        public void CreateSlots(int amountSlots)
        {
            _slots = new SlotView[amountSlots];
            
            for(int i = 0; i < amountSlots; i++)
            {
                _slots[i] = Instantiate(_slotViewPrefab, _inventoryGrid);
                _slots[i].onItemChanged += onItemChanged;
            }
        }

        public void FillGrid(Item[] items)
        {
            var length = items.Length;

            if (length > _slots.Length) return;

            for(int i = 0; i < length; i++)
            {
                if (items[i] != null)
                    _slots[i].SetItem(items[i]);
            }
        }

        private void ClearGrid()
        {
            foreach (var slot in _slots)
                slot.Clear();
        }
    }
}