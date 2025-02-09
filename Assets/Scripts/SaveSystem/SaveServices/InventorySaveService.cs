using System.Linq;
using Inventory;
using SaveSystem.Main;
using UnityEngine;

namespace SaveSystem.SaveServices
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

            foreach (var item in saveData.inventoryItems)
                item.PrintInfo();

            Debug.Log("All inventory items saved");
            return saveData;
        }

        public void Load(SaveData saveData)
        {
            foreach (var item in saveData.inventoryItems)
                item.PrintInfo();

            _inventoryService.LoadData(saveData.inventoryItems.ToArray());
            Debug.Log("All inventory items loaded");
        }
    }
}