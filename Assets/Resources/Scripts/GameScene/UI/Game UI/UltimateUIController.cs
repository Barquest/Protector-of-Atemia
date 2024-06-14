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
        [SerializeField] private TextMeshProUGUI useSkillText;
        bool available;
        bool isActivated;

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
                available = true;
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
            if (!isActivated) return;
            
            OnSkillButtonPressed?.Invoke();
            available = false;
            useSkillButton.gameObject.SetActive(false);
        }
        public void DisactivateSkillButton()
        {
           // if (!available) return;
            useSkillButton.image.color = Color.grey;
            useSkillText.color = Color.grey;
            isActivated = false;
        }
        public void ActivateSkillButton()
        {
           // if (!available) return;
            useSkillButton.image.color = Color.white;
            useSkillText.color = Color.white;
            isActivated = true;
        }
    }
}
