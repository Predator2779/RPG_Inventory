using System;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopupManagement.Popups
{
    public class ItemPopup : BasePopup
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemName, _itemDefense, _itemWeight;
        [SerializeField] private Button _actionButton, _closeButton;

        protected override void Setup(object data)
        {
            if (data is Item item)
            {
                _itemIcon.sprite = item.Icon;
                _itemName.text = item.Name;
                _itemDefense.text = $"+{item.Defense}";
                _itemWeight.text = $"{item.Weight:0.00} кг";

                SetActionButton(item.Type);

                _closeButton.onClick.RemoveAllListeners();
                _closeButton.onClick.AddListener(Close);
            }
            else
            {
                Debug.LogError("Invalid data type for ItemPopup");
            }
        }

        private void SetActionButton(ItemType type)
        {
            _actionButton.onClick.RemoveAllListeners();
            _actionButton.onClick.AddListener(() => Debug.Log("Used"));
            
            switch (type)
            {
                case ItemType.Ammo:
                    _actionButton.onClick.AddListener(() => Debug.Log("Used"));
                    break;
                case ItemType.Consumable:
                    break;
                case ItemType.Head:
                    break;
                case ItemType.Torso:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}