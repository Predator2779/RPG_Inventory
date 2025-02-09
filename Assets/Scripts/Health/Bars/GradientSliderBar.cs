using UnityEngine;

namespace Health.Bars
{
    public class GradientSliderBar : SliderBar
    {
        [SerializeField] private Gradient _gradient;

        public override void SetCurrentValue(float value)
        {
            base.SetCurrentValue(value);
            _fill.color = _gradient.Evaluate(_slider.normalizedValue);
        }
    }
}