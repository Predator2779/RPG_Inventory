﻿using System;
using Inventory.Actions;
using Inventory.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Health;
using Inventory.Services;

namespace PopupManagement.Popups
{
    public class ItemPopup : BasePopup
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemName, _itemDefense, _itemWeight;
        [SerializeField] private Button _actionButton, _removeButton, _closeButton;
            
        private HealthProcessor _healthProcessor;
        private IItemAction _currentAction;
        private IInventoryService _inventoryService;
        private RemoveAction _removeAction;
        
        public void Initialize(IInventoryService inventoryService, HealthProcessor healthProcessor)
        {
            _inventoryService = inventoryService;
            _healthProcessor = healthProcessor;
            _removeAction = new RemoveAction(_inventoryService);
        }

        public override void Show(object data)
        {
            if (data is not ItemData) return;
            base.Show(data);
        }

        protected override void Setup(object data)
        {
            if (data is ItemData item)
            {
                _itemIcon.sprite = item.Icon;
                _itemName.text = item.Name;
                _itemDefense.text = $"+{item.Defense}";
                _itemWeight.text = $"{item.Weight * item.Stack:0.00} кг";

                _currentAction = GetItemAction(item.Type);
                _actionButton.GetComponentInChildren<TMP_Text>().text = _currentAction.ButtonText;
                _closeButton.GetComponentInChildren<TMP_Text>().text = "Закрыть";

                _actionButton.onClick.RemoveAllListeners();
                _actionButton.onClick.AddListener(() => _currentAction.Execute(item));
                _actionButton.onClick.AddListener(Close);

                _removeButton.onClick.RemoveAllListeners();
                _removeButton.onClick.AddListener(() => _removeAction.Execute(item));
                _removeButton.onClick.AddListener(Close);
                
                _closeButton.onClick.RemoveAllListeners();
                _closeButton.onClick.AddListener(Close);
            }
            else
            {
                Debug.LogError("Invalid data type for ItemPopup");
            }
        }
        
        private IItemAction GetItemAction(ItemType type)
        {
            switch (type)
            {
                case ItemType.Ammo: return new AmmoAction(_inventoryService);
                case ItemType.Consumable: return new MedKitAction(_inventoryService, _healthProcessor);
                case ItemType.Head:
                case ItemType.Body: return new EquipAction(_inventoryService);
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}