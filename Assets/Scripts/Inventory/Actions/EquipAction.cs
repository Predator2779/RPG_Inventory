using Inventory.Items;
using UnityEngine;

namespace Inventory.Actions
{
    public class EquipAction : IItemAction
    {
        private readonly IInventoryService _inventoryService;

        public string ButtonText => "Экипировать";

        public EquipAction(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void Execute(Item item)
        {
            _inventoryService.EquipItem(item);
        }
    }
}