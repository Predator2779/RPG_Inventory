using Equipment;
using Health;
using Inventory.Services;
using PopupManagement.Factory;
using PopupManagement.Popups;
using SaveSystem;
using UnityEngine;

namespace StartGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private StartInventorySetting _startInventorySetting;
        [SerializeField] private SaveSetting _saveSetting;
        [SerializeField] private FactorySetting _factorySetting;
        [SerializeField] private EquipmentSetting _equipmentSetting;
        [SerializeField] private HealthProcessor _heroHealthProcessor;
        
        private void Awake()
        {
            var factory = _factorySetting.InitializeFactory();
            var itemPopup = factory.GetPopup<ItemPopup>();

            var inventoryService = new InventoryService();
            
            _startInventorySetting.InitializeInventory(itemPopup);
            
            var equipmentController = _equipmentSetting.Initialize(inventoryService);
            inventoryService.SetControllers(_startInventorySetting.InventoryController, equipmentController);
            itemPopup.Initialize(inventoryService, _heroHealthProcessor);

            _saveSetting.Initialize(inventoryService, _equipmentSetting.EquipSlots.ToArray());
            _startInventorySetting.AddAdditionalItems();
        }
    }
}