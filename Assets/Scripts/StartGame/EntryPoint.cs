using Equipment;
using GameState;
using Inventory.Services;
using PopupManagement.Factory;
using PopupManagement.Popups;
using SaveSystem;
using UnityEngine;

namespace StartGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameStateSetting _gameStateSetting;
        [SerializeField] private StartInventorySetting _startInventorySetting;
        [SerializeField] private SaveSetting _saveSetting;
        [SerializeField] private FactorySetting _factorySetting;
        [SerializeField] private EquipmentSetting _equipmentSetting;
        
        private void Awake()
        {
            var factory = _factorySetting.InitializeFactory();
            var itemPopup = factory.GetPopup<ItemPopup>();
            var endPopup = factory.GetPopup<EndPopup>();
            var inventoryService = new InventoryService();
            
            _startInventorySetting.InitializeInventory(itemPopup);

            var equipmentController = _equipmentSetting.Initialize(inventoryService);
            inventoryService.SetControllers(_startInventorySetting.InventoryController, equipmentController);
            var gameStateController = _gameStateSetting.Initialize(inventoryService, endPopup);
            var (heroHealth, enemyHealth) = _gameStateSetting.GetHealths();
            itemPopup.Initialize(inventoryService, heroHealth);

            _saveSetting.Initialize(inventoryService, _equipmentSetting.EquipSlots.ToArray());
            _startInventorySetting.AddAdditionalItems();
        }
    }
}