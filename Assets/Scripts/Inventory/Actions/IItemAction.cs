using Inventory.Items;

namespace Inventory.Actions
{
    public interface IItemAction
    {
        public string ButtonText { get; }
        
        public void Execute(ItemData itemData);  
    }
}