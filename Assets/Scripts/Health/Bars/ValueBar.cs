using TMPro;
using UnityEngine;

namespace Health.Bars
{
    public class ValueBar : ViewBar
    {
        [SerializeField] private TMP_Text _countText;
        
        public override void SetCurrentValue(float value)
        {
            _countText.text = value.ToString();
        }
    }
}