using Health;
using UnityEngine;

namespace BattleSystem
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private HealthProcessor _enemyHealth;
        
        private IWeapon _currentWeapon;

        public void ChangeWeapon(IWeapon weapon) => _currentWeapon = weapon;
        public void Shoot() => _currentWeapon?.Shoot(_enemyHealth);
    }
}