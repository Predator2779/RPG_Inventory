using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class SlotView : MonoBehaviour
    {
        public Action<int, int> OnDragItem;
        
        [SerializeField] private Item _сurrentItem;
        [SerializeField] private Image _imageItem;   
        [SerializeField] private TMP_Text _amountText;
        
        public int Index { get; set; }
        
        private void OnEnable() => SlotActive(false);
        public void DragFrom(int slotIndex) => OnDragItem?.Invoke(slotIndex, Index);

        public void SetItem(Item item)
        {
            SlotActive(true);
            
            _сurrentItem = item;
        
            _imageItem.sprite = _сurrentItem.Icon;
            _imageItem.gameObject.SetActive(_сurrentItem.Icon != null);
            _amountText.text = _сurrentItem.Stack > 1 ? _сurrentItem.Stack.ToString() : "";
        }

        public void Clear()
        {
            _сurrentItem = null;
            _imageItem.sprite = null;
            _amountText.text = "";
        
            SlotActive(false);
        }

        private void SlotActive(bool value)
        {
            _imageItem.gameObject.SetActive(value);
            _amountText.gameObject.SetActive(value);
        }
    }
}