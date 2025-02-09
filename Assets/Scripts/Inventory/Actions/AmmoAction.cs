using Inventory.Items;
using UnityEngine;

namespace Inventory.Actions
{
    public class AmmoAction : IItemAction
    {
        private IInventoryService _inventoryService;
        
        public string ButtonText => "Купить";

        public AmmoAction(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void Execute(Item item)
        {
            _inventoryService.AddItem(item);
        }
    }
}