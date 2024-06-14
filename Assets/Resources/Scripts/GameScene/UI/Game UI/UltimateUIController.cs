using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;
using DentedPixel;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class UltimateUIController : MonoBehaviour
    {
        [SerializeField] private Slider ultimateSlider;
        [SerializeField] private Image sliderFill;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color maxColor;
        [SerializeField] private Button useSkillButton;

		public event Action OnSkillButtonPressed;

		private void Start()
		{
            UpdateSliderValue(0, 1);
            useSkillButton.gameObject.SetActive(false);
        }
        public void UpdateSliderValue(float val,float maxVal)
        {
            ultimateSlider.value = val / maxVal;
            if (val == maxVal)
            {
                sliderFill.color = maxColor;
                useSkillButton.gameObject.SetActive(true);
            }
            else {
                sliderFill.color = defaultColor;
            }
        }
		private void OnDestroy()
		{
            OnSkillButtonPressed = null;
		}
		public void SkillButtonPressed()
        {
            OnSkillButtonPressed?.Invoke();
            useSkillButton.gameObject.SetActive(false);
        }
    }
}
