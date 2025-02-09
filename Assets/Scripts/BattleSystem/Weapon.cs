using Health;
using UnityEngine;

namespace BattleSystem
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private WeaponManager _weaponManager;
        [SerializeField] private float _damage;

        public void ChangeWeapon()
        {
            _weaponManager.ChangeWeapon(this);
        }

        public void Shoot(HealthProcessor enemyHealth)
        {
            enemyHealth.TakeDamage(_damage);
        }
    }
}