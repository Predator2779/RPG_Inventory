using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _inventoryGrid;
    [SerializeField] private Slot _slotPrefab;
    [SerializeField] private InventoryData _inventoryData;

    private const int TotalSlots = 30;

    private Slot[] _slots = new Slot[TotalSlots];

    private void Start()
    {
        CreateSlots();
        FillGrid(_inventoryData.items.ToArray());
    }

    private void CreateSlots()
    {
        for(int i = 0; i < TotalSlots; i++)
            _slots[i] = Instantiate(_slotPrefab, _inventoryGrid);
    }

    public void FillGrid(Item[] items)
    {
        var length = items.Length;

        if (length > TotalSlots) return;

        for(int i = 0; i < length; i++)
            if (items[i] != null)
                _slots[i].SetItem(items[i]);
    }
}