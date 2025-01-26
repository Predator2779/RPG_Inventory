using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image _imageItem;   
    [SerializeField] private TMP_Text _amountText;

    private Item _currentItem;

    private void OnEnable()
    {
        SlotActive(_currentItem != null);
    }

    public void SetItem(Item item)
    {
        _currentItem = item;
    }

    public Item GetItem()
    {
        return _currentItem;
    }

    public void ClearSlot()
    {
        SetItem(null);
    }

    private void SlotActive(bool value)
    {
        _imageItem.gameObject.SetActive(value);
        _amountText.gameObject.SetActive(value);
    }
}