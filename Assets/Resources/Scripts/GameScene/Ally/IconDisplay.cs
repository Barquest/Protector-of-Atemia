using System;
using UnityEngine;
using UnityEngine.UI;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class IconDisplay : MonoBehaviour
    {
        [SerializeField] private bool isEmpty;
        [SerializeField] private bool isActivated;
        [SerializeField] private bool isAvailable;
        [SerializeField] private Image icon;
        [SerializeField] private Image background;

        public event Action OnClick;

        public void DisplayUI(float value)
        {
            icon.fillAmount = value;
            if (value >= 1)
            {
                if (!isActivated)
                    isActivated = true;
            }
            else {
                if (isActivated) 
                    isActivated = false;
            }
        }
        public void OnClickIcon()
        {
            if (isActivated && isAvailable)
            {
                OnClick?.Invoke();
                isActivated = false;
                DisplayUI(0);
            }
            Debug.Log("TryClick");
        }
        public void Disactivate()
        {
            if (isActivated)
            {
                isAvailable = false;
                icon.color = Color.grey;
            }
        }
        public void Activate()
        {
            if (isActivated)
            {
                isAvailable = true;
                icon.color = Color.white;
            }
        }
        public void ForceActivate()
        {
            isAvailable = true;
        }
        private void OnDestroy()
		{
            OnClick = null;
		}

	}
}
