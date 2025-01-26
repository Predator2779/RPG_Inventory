using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] public Image _imageItem;   
    [SerializeField] private TMP_Text _amountText;

    private Item _currentItem;

    private void Start()
    {
        SlotActive(false);
    }

    public Item GetItem() => _currentItem;

    public void SetItem(Item item)
    {
        SlotActive(true);
        _currentItem = item;
        
        _imageItem.sprite = _currentItem.Icon;
        _imageItem.gameObject.SetActive(true);
            
        _amountText.text = _currentItem.Stack > 1 ? _currentItem.Stack.ToString() : "";
        _amountText.gameObject.SetActive(_currentItem.Stack > 1);
    }

    public void ClearSlot()
    {
        _currentItem = null;
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