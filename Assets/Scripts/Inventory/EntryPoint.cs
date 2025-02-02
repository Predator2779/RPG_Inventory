using UnityEngine;

namespace Inventory
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private StartInventorySetting _startInventorySetting;

        private void Awake() => _startInventorySetting.InitializeInventory();
    }
}