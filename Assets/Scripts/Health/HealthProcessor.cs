using System;
using Health.Bars;
using UnityEngine;

namespace Health
{
    public class HealthProcessor : MonoBehaviour
    {
        public Action OnHealthIsZero;
        
        [SerializeField] private ViewBar[] _viewBars;
        [SerializeField] private float _maxHitPoints;

        private ValueStorage _health;

        private void Awake()
        {
            _health = new ValueStorage(_maxHitPoints, _maxHitPoints, _viewBars);
            _health.OnHealthIsZero += () => OnHealthIsZero?.Invoke();
        }
        
        public void TakeHeal(float value) => _health.Increase(value);
        public void TakeDamage(float value)=> _health.Decrease(value);
    }
}