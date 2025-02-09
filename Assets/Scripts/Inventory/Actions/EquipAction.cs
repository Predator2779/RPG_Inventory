using Inventory.Items;
using Inventory.Services;

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

        public void Execute(ItemData itemData)
        {
            _inventoryService.EquipItem(itemData);
        }
    }
}