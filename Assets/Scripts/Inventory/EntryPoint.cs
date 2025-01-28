using UnityEngine;

namespace Inventory
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData;
        [SerializeField] private InventoryView _inventoryView;

        private InventoryPresenter _inventoryPresenter;

        private void Awake()
        {
            _inventoryPresenter = new InventoryPresenter();
            _inventoryPresenter.Initialize(_inventoryView, _inventoryData);
        }
    }
}