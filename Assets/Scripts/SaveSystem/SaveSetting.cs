using System;
using System.Linq;
using Equipment;
using Inventory.Services;
using SaveSystem.Main;
using SaveSystem.Services;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class SaveSetting
    {
        [SerializeField] private GameStateSaver _gameStateSaver;

        public void Initialize(InventoryService inventoryService, EquipSlot[] equipSlots)
        {
            var inventorySaveService = new InventorySaveService(_gameStateSaver, inventoryService);
            var equipSaveService = new EquipSaveService(_gameStateSaver, equipSlots.ToArray());
            _gameStateSaver.Init();
        }
    }
}