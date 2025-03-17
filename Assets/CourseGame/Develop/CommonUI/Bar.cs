using UnityEngine;
using UnityEngine.UI;

namespace Assets.CourseGame.Develop.CommonUI
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _filler;

        public void UpdateValue(float sliderValue) => _slider.value = sliderValue;
        public void SetFillerColor(Color color) => _filler.color = color;
    }
}

