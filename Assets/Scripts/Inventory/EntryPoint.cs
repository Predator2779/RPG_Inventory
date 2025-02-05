using PopupManagement;
using PopupManagement.Factory;
using PopupManagement.Popups;
using UnityEngine;

namespace Inventory
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private StartInventorySetting _startInventorySetting;
        [SerializeField] private FactorySetting _factorySetting;

        private void Awake()
        {
            var factory = _factorySetting.InitializeFactory();
            _startInventorySetting.InitializeInventory(factory.GetPopup<ItemPopup>());
        }
    }
}