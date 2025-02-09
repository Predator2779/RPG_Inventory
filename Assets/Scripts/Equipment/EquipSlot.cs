using System;
using Inventory.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Equipment
{
    public class EquipSlot : MonoBehaviour
    {
        [SerializeField] private ItemType _slotType;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _defenseText;

        private Item _equippedItem;

        private void Awake()
        {
            print($"{name} slot is null: {_equippedItem is null}");
        }

        public ItemType SlotType => _slotType;
        public Item GetEquippedItem() => _equippedItem;

        public void Equip(Item item)
        {
            print("Equip action invoked!");
            
            if (item.Type != _slotType)
            {
                Debug.LogError($"Нельзя экипировать {item.Name} в слот {_slotType}!");
                return;
            }

            _equippedItem = item;
            _itemIcon.sprite = item.Icon;
            _itemIcon.gameObject.SetActive(true);
            _defenseText.text = $"+{item.Defense}";

            Debug.Log($"Экипирован: {item.Name}");
        }

        public Item Unequip()
        {
            if (_equippedItem == null) return null;

            var previousItem = _equippedItem;
            _equippedItem = null;
            _itemIcon.gameObject.SetActive(false);
            _defenseText.text = "";

            Debug.Log($"Снято: {previousItem.Name}");
            return previousItem;
        }
    }
}