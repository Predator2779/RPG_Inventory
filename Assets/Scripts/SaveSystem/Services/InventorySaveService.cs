using System.Linq;
using Inventory.Services;
using SaveSystem.Main;
using UnityEngine;

namespace SaveSystem.Services
{
    public class InventorySaveService : ISaveService
    {
        private readonly InventoryService _inventoryService;

        public InventorySaveService(
            GameStateSaver gameStateSaver,
            InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            gameStateSaver.RegisterSaver(this);
        }

        public SaveData Save(SaveData saveData)
        {
            saveData.inventoryItems = _inventoryService.GetInventoryData().ToList();
            Debug.Log("All inventory items saved");
            return saveData;
        }

        public void Load(SaveData saveData)
        {
            _inventoryService.LoadData(saveData.inventoryItems.ToArray());
            Debug.Log("All inventory items loaded");
        }
    }
}