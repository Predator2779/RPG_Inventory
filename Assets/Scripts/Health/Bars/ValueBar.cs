using UnityEngine;
using UnityEngine.UI;

namespace Health.Bars
{
    public class ValueBar : MonoBehaviour
    {
        [SerializeField] protected Slider _slider;
        [SerializeField] protected Image _fill;
      
        public virtual void SetCurrentValue(float value) => _slider.value = value;
    }
}