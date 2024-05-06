using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class BarView : MonoBehaviour
    {
        [SerializeField]
        private Slider _barSlider;
        
        private const float DecimalToPercent = 0.01f;
        
        public void ChangeValue(int value)
        {
            _barSlider.value = value * DecimalToPercent;
        }
    }
}