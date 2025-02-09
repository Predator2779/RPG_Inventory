using System;
using System.Collections.Generic;
using Inventory.Items;
using Inventory.Main;
using Inventory.Slots;
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
        [SerializeField] private bool _add;
        [SerializeField] private List<ItemData> _additionalItemDatas = new ();
        
        public InventoryController InventoryController { get; private set; }
        public InventoryData InventoryData { get; private set; }
        public InventoryView InventoryView { get; private set; }

        public void InitializeInventory(ItemPopup popup)
        {
            _inventoryGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _inventoryGrid.constraintCount = _columnAmount;
            
            InventoryData = new InventoryData();
            InventoryView = new InventoryView(_inventoryGrid.transform, _slotPrefab);
            InventoryController = new InventoryController(InventoryView, InventoryData, popup, _totalAmount);
            InventoryController.Initialize();
        }

        public void AddAdditionalItems()
        {
            if (!_add) return;

            foreach (var data in _additionalItemDatas)
                InventoryController.AddItem(data.item);
        }
    }
}