using UnityEngine;
using UnityEngine.UI;

namespace Health.Bars
{
    public class SliderBar : ViewBar
    {
        [SerializeField] protected Slider _slider;
        [SerializeField] protected Image _fill;
      
        public override void SetCurrentValue(float value) => _slider.value = value / 100;
    }
}