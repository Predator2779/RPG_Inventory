using System;
using Inventory.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory.Slots
{
    [RequireComponent(typeof(Button))]
    public class SlotView : MonoBehaviour
    {
        public Action<int, int> OnDragItem;
        public Action<ItemData> OnItemUse;
        
        [FormerlySerializedAs("_currentItemDat")] [FormerlySerializedAs("_currentItem")] [SerializeField] private ItemData _currentItemData;
        [SerializeField] private Image _imageItem;   
        [SerializeField] private TMP_Text _amountText;

        private Button _button;
        
        public int Index { get; set; }

        private void OnValidate() => InitializeButton();

        private void Awake()
        {
            InitializeButton();
            SlotActive(false);
        }
        
        public void DragFrom(int slotIndex) => OnDragItem?.Invoke(slotIndex, Index);

        public void SetItem(ItemData itemData)
        {
            SlotActive(true);
            _currentItemData = itemData;
            _imageItem.sprite = _currentItemData.Icon;
            _imageItem.gameObject.SetActive(_currentItemData.Icon != null);
            _amountText.text = _currentItemData.Stack > 1 ? _currentItemData.Stack.ToString() : "";
        }

        public void Clear()
        {
            _currentItemData = null;
            _imageItem.sprite = null;
            _amountText.text = "";
            SlotActive(false);
        }

        private void SlotActive(bool value)
        {
            _imageItem.gameObject.SetActive(value);
            _amountText.gameObject.SetActive(value);
            _button.interactable = value;
        }

        private void InitializeButton()
        {
            _button ??= GetComponent<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(Use);
        }

        private void Use() => OnItemUse?.Invoke(_currentItemData);
    }
}