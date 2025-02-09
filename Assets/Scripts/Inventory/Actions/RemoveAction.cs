using Inventory.Items;
using Inventory.Services;

namespace Inventory.Actions
{
    public class RemoveAction : IItemAction
    {
        private readonly IInventoryService _inventoryService;

        public string ButtonText => "Удалить";

        public RemoveAction(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public void Execute(ItemData itemData)
        {
            _inventoryService.RemoveItem(itemData.Index);
        }
    }
}