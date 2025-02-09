using System;
using BattleSystem;
using Equipment;
using Health;
using Inventory.Items;
using Inventory.Services;
using PopupManagement.Popups;
using UnityEngine;

namespace GameState
{
    [Serializable]
    public class GameStateSetting
    {
        [Header("Main Components")]
        [SerializeField] private WeaponManager _weaponManager;

        [Header("Heroes")] 
        [SerializeField] public HealthProcessor _heroHealth;
        [SerializeField] public HealthProcessor _enemyHealth;
        
        [Header("Loot")]
        [SerializeField] private Item[] _loot;

        public GameStateController Initialize(InventoryService inventoryService, EndPopup endPopup, EquipSlot[] equipSlots)
        {
            var gameStateController = new GameStateController(inventoryService, endPopup, _heroHealth, _enemyHealth, _loot);
            _weaponManager.Initialize(inventoryService, _heroHealth, _enemyHealth, equipSlots);
            return gameStateController;
        }

        public (HealthProcessor, HealthProcessor) GetHealths()
        {
            return (_heroHealth, _enemyHealth);
        }
    }
}