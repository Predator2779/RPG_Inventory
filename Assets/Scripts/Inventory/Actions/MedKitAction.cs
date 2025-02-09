using Health;
using Inventory.Items;
using Inventory.Services;
using UnityEngine;

namespace Inventory.Actions
{
    public class MedKitAction : IItemAction
    {
        private const float HealPoints = 50;
        
        private readonly IInventoryService _inventoryService;
        private readonly HealthProcessor _health;

        public string ButtonText => "Лечить";

        public MedKitAction(IInventoryService inventoryService, HealthProcessor health)
        {
            _inventoryService = inventoryService;
            _health = health;
        }

        public void Execute(ItemData itemData)
        {
            if (itemData.Stack > 0)
            {
                itemData.Stack--;
                _health.TakeHeal(HealPoints);
                _inventoryService.DrawItems();
                Debug.Log($"MedKit used. Left: {itemData.Stack}");
            }
            else
            {
                Debug.LogWarning("MedKits are out!");
            }
        }
    }
}