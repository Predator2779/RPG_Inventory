using Health.Bars;
using UnityEngine;

namespace Health
{
    public class ValueStorage
    {
        private const float MinValue = 0;
        private readonly ViewBar[] _viewBars;
        private readonly float _maxValue;
        private float _currentValue;

        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = Mathf.Clamp(value, MinValue, _maxValue == 0 ? value : _maxValue);
                ChangeBar();
            }
        }

        public ValueStorage(float currentValue, ViewBar[] viewBars)
        {
            _viewBars = viewBars;
            _maxValue = 0;
            CurrentValue = currentValue;
        }

        public ValueStorage(float currentValue, float maxValue, ViewBar[] viewBars)
        {
            _viewBars = viewBars;
            _maxValue = maxValue;
            CurrentValue = currentValue;
        }

        public void Increase(float value) => CurrentValue += value;
        public void Decrease(float value) => CurrentValue -= value;
        private float GetPercentageRation() => CurrentValue / _maxValue * 100;

        private void ChangeBar()
        {
            if (_viewBars.Length <= 0) return;

            foreach (var viewBar in _viewBars)
                viewBar.SetCurrentValue(GetPercentageRation());
        }
    }
}