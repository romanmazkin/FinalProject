using TMPro;
using UnityEngine;

namespace Assets.CourseGame.Develop.CommonUI
{
    public class BarWithText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Bar _bar;

        public void UpdateText(string text) => _text.text = text;

        public void UpdateSlider(float sliderValue) => _bar.UpdateValue(sliderValue);
        public void SetFillerColor(Color color) => _bar.SetFillerColor(color);
    }
}
