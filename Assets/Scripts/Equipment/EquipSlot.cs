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

        private ItemData _equippedItemData;
        
        public ItemType SlotType => _slotType;
        public ItemData GetEquippedItem() => _equippedItemData;

        public void Equip(ItemData itemData)
        {
            if (itemData.Type != _slotType)
            {
                Debug.LogError($"Cannot equip {itemData.Name} to slot {_slotType}!");
                return;
            }

            _equippedItemData = itemData;
            _itemIcon.sprite = itemData.Icon;
            _itemIcon.gameObject.SetActive(true);
            _defenseText.text = $"+{itemData.Defense}";

            Debug.Log($"Equip item: {itemData.Name}");
        }

        public ItemData Unequip()
        {
            if (_equippedItemData == null) return null;

            var previousItem = _equippedItemData;
            _equippedItemData = null;
            _itemIcon.gameObject.SetActive(false);
            _defenseText.text = "";

            Debug.Log($"Unequipped item: {previousItem.Name}");
            return previousItem;
        }
    }
}