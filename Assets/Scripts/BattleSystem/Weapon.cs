using Health;
using Inventory.Items;
using UnityEngine;

namespace BattleSystem
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private WeaponManager _weaponManager;
        [SerializeField] private Item _item;
        [SerializeField] private float _damage;
        [SerializeField] private int _amountAmmo;

        public void ChangeWeapon()
        {
            _weaponManager.ChangeWeapon(this);
        }

        public void Shoot(HealthProcessor enemyHealth, ItemData ammo)
        {
            enemyHealth.TakeDamage(_damage);
            ammo.Stack -= _amountAmmo;
            Debug.Log($"The hero is shooted! Damage: {_damage}");
        }

        public (string, int) AmmoTypeAndAmount() => (_item.data.Name, _amountAmmo);
    }
}