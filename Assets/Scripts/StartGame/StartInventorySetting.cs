using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Items;
using Inventory.Main;
using PopupManagement.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace StartGame
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
        [SerializeField] public List<ItemData> _additionalItems = new ();
        
        public InventoryController InventoryController { get; private set; }
        public InventoryData InventoryData { get; private set; }
        public InventoryView InventoryView { get; private set; }

        public void InitializeInventory(ItemPopup popup)
        {
            _inventoryGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _inventoryGrid.constraintCount = _columnAmount;
            
            InventoryData = new InventoryData(InitializeArray());
            InventoryView = new InventoryView(_inventoryGrid.transform, _slotPrefab);
            InventoryController = new InventoryController(InventoryView, InventoryData, popup, _totalAmount);
            InventoryController.Initialize();
        }

        private Item[] InitializeArray()
        {
            var length = _additionalItems.Count;
            var arr = new Item[length];

            for(int i = 0; i < length; i++)
            {
                arr[i] = _additionalItems[i].item;
                arr[i].Index = i;
            }

            return arr;
        }
    }
}