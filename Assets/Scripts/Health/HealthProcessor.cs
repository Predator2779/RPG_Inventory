using System;
using Health.Bars;
using UnityEngine;

namespace Health
{
    public class HealthProcessor : MonoBehaviour
    {
        [SerializeField] private ViewBar[] _viewBars;
        [SerializeField] private float _maxHitPoints;

        private ValueStorage _health;
        
        private void Awake() => _health = new ValueStorage(_maxHitPoints, _maxHitPoints, _viewBars);
        public void TakeHeal(float value) => _health.Increase(value);
        public void TakeDamage(float value)=> _health.Decrease(value);
    }
}