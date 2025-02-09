using Inventory.Items;
using Inventory.Services;

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

        public void Execute(ItemData itemData)
        {
            _inventoryService.BuyItem(itemData.Index);
        }
    }
}