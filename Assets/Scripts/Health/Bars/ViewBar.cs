using UnityEngine;

namespace Health.Bars
{
    public abstract class ViewBar : MonoBehaviour
    {
        public abstract void SetCurrentValue(float value);
    }
}