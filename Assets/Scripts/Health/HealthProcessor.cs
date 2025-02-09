using System;
using Health.Bars;
using UnityEngine;

namespace Health
{
    public class HealthProcessor : MonoBehaviour
    {
        [SerializeField] private ValueBar _valueBar;
        [SerializeField] private float _maxHitPoints;
        [SerializeField][Range(0, 100)] private float _currentHitPoints;

        private ValueStorage _health;
        
        private void Awake() => _health = new ValueStorage(_maxHitPoints, _maxHitPoints, _valueBar);
        private void Update() => _health.CurrentValue = _currentHitPoints;
        public void TakeHeal(float value) => _health.Increase(value);
        public void TakeDamage(float value)=> _health.Decrease(value);
    }
}