using Equipment;
using Health;
using Inventory.Items;
using Inventory.Services;
using UnityEngine;

namespace BattleSystem
{
    public class WeaponManager : MonoBehaviour
    {
        private const float EnemyDamage = 15;
        private InventoryService _inventoryService;
        private HealthProcessor _heroHealth, _enemyHealth;
        private EquipSlot[] _equipSlots;
        private IWeapon _currentWeapon;
        private bool _canHeadShot;
        
        public void Initialize(InventoryService inventoryService, HealthProcessor heroHealth, HealthProcessor enemyHealth, EquipSlot[] equipSlots)
        {
            (_inventoryService, _heroHealth, _enemyHealth, _equipSlots) = (inventoryService, heroHealth, enemyHealth, equipSlots);
        }
        
        public void ChangeWeapon(IWeapon weapon) => _currentWeapon = weapon;

        public void Shoot()
        {
            if (!CanShoot(out var item))
            {
                Debug.Log("Not enough ammo!");
                return;
            }
            
            _currentWeapon?.Shoot(_enemyHealth, item);
            _inventoryService.DrawItems();
            StrikeBack();
        }

        private bool CanShoot(out ItemData item)
        {
            item = null;
            if (_currentWeapon == null) return false;
            var (type, amount) = _currentWeapon.AmmoTypeAndAmount();
            return _inventoryService.TryGetItem(type, amount, out item);
        }

        private void StrikeBack()
        {
            var (type, damage) = GetDamage();
            _heroHealth.TakeDamage(damage);
            Debug.Log($"The enemy strikes back! Strike to {type}; Damage: {damage}");
            _canHeadShot = !_canHeadShot;
        }

        private (ItemType, float) GetDamage()
        {
            ItemType type = _canHeadShot ? ItemType.Head : ItemType.Body;
            float defense = 0;
            foreach (var slot in _equipSlots)
            {
                if (slot.SlotType == type)
                {
                    var item = slot.GetEquippedItem();
                    if (item != null)
                    {
                        defense = item.Defense;
                        break;
                    }
                }
            }

            return (type, Mathf.Clamp(EnemyDamage - defense, 0, EnemyDamage));
        }
    }
}