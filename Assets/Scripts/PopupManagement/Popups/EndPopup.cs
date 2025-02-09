using System;
using Health;
using Inventory.Actions;
using Inventory.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopupManagement.Popups
{
    public class EndPopup : BasePopup
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button  _closeButton;

        public override void Show(object data)
        {
            if (data is not Item) return;
            base.Show(data);
        }

        protected override void Setup(object data)
        {
            if (data is string message)
            {
                // _itemIcon.sprite = item.Icon;
                _text.text = message;
                _closeButton.GetComponentInChildren<TMP_Text>().text = "Закрыть";
                _closeButton.onClick.RemoveAllListeners();
                _closeButton.onClick.AddListener(Close);
            }
            else
            {
                Debug.LogError("Invalid data type for ItemPopup");
            }
        }
    }
}