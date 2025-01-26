using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _inventoryGrid;
    [SerializeField] private Slot _slotPrefab;

    private const int TotalSlots = 30;

    private void Start()
    {
        for (int i = 0; i < TotalSlots; i++)
        {
            Instantiate(_slotPrefab, _inventoryGrid);
        }
    }
}