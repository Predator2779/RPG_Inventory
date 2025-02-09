using Health.Bars;
using UnityEngine;

namespace Health
{
    public class ValueStorage
    {
        private const float MinValue = 0;
        private readonly ValueBar _valueBar;
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

        public ValueStorage(float currentValue, ValueBar valueBar)
        {
            _maxValue = 0;
            CurrentValue = currentValue;
            _valueBar = valueBar;
        }

        public ValueStorage(float currentValue, float maxValue, ValueBar valueBar)
        {
            _maxValue = maxValue;
            CurrentValue = currentValue;
            _valueBar = valueBar;
        }

        public void Increase(float value) => CurrentValue += value;
        public void Decrease(float value) => CurrentValue -= value;
        private float GetPercentageRation() => CurrentValue / _maxValue;

        private void ChangeBar()
        {
            if (_valueBar != null) _valueBar.SetCurrentValue(GetPercentageRation());
        }
    }
}