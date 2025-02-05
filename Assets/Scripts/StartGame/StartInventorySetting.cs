using System;
using System.Collections.Generic;
using PopupManagement.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    [Serializable]
    public class StartInventorySetting
    {
        [Header("Number of inventory slots")]
        [SerializeField] private int _columnAmount, _totalAmount;
        
        [Header("Required components")]
        [SerializeField] private GridLayoutGroup _inventoryGrid;
        [SerializeField] private SlotView _slotPrefab;

        [Header("Start list items")]
        [SerializeField] private List<Item> _list = new ();
        
        [SerializeField] private InventoryData _inventoryData;
        
        private InventoryController _inventoryController;
        private InventoryView _inventoryView;

        public void InitializeInventory(ItemPopup itemPopup)
        {
            _inventoryGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _inventoryGrid.constraintCount = _columnAmount;
            
            _inventoryData = new InventoryData(InitializeArray());
            _inventoryView = new InventoryView(_inventoryGrid.transform, _slotPrefab);
            _inventoryController = new InventoryController(_inventoryView, _inventoryData, itemPopup, _totalAmount);
            
            _inventoryController.Initialize();
        }

        private Item[] InitializeArray()
        {
            var length = _list.Count;
            var arr = _list.ToArray();
            
            for(int i = 0; i < length; i++)
                arr[i].Index = i;

            return arr;
        }
    }
}