using Health;

namespace BattleSystem
{
    public interface IWeapon
    {
        public void Shoot(HealthProcessor enemyHealth);
    }
}