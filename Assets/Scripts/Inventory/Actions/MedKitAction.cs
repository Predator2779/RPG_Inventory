using Health;
using Inventory.Items;
using Inventory.Services;
using UnityEngine;

namespace Inventory.Actions
{
    public class MedKitAction : IItemAction
    {
        private readonly IInventoryService _inventoryService;
        private readonly HealthProcessor _health;

        public string ButtonText => "Лечить";

        public MedKitAction(IInventoryService inventoryService, HealthProcessor health)
        {
            _inventoryService = inventoryService;
            _health = health;
        }

        public void Execute(Item item)
        {
            if (item.Stack > 0)
            {
                _health.Heal(50);
                item.Stack--;
                Debug.Log($"Использована аптечка, осталось: {item.Stack}");
            }
            else
            {
                Debug.LogWarning("Аптечки закончились!");
            }
        }
    }
}