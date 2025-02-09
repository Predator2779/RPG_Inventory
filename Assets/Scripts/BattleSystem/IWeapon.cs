using Health;
using Inventory.Items;

namespace BattleSystem
{
    public interface IWeapon
    {
        public void Shoot(HealthProcessor enemyHealth, ItemData ammo);
        public (string, int) AmmoTypeAndAmount();
    }
}