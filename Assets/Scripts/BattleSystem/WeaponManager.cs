using Health;
using UnityEngine;

namespace BattleSystem
{
    public class WeaponManager : MonoBehaviour
    {
        private HealthProcessor _heroHealth, _enemyHealth;
        private IWeapon _currentWeapon;
        private bool _canHeadShot;
        
        public void Initialize(HealthProcessor heroHealth, HealthProcessor enemyHealth)
        {
            (_heroHealth, _enemyHealth) = (heroHealth, enemyHealth);
        }
        
        public void ChangeWeapon(IWeapon weapon) => _currentWeapon = weapon;

        public void Shoot()
        {
            _currentWeapon?.Shoot(_enemyHealth);
            StrikeBack();
        }

        private void StrikeBack()
        {
            if (_canHeadShot) _heroHealth.TakeDamage(15);
            else _heroHealth.TakeDamage(15);

            Debug.Log("The enemy strikes back!");
            
            _canHeadShot = !_canHeadShot;
        }
    }
}