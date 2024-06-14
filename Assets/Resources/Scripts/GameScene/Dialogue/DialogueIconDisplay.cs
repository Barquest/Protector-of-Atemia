using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IconPosition 
{
    left,middle,right
}

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class DialogueIconDisplay : MonoBehaviour
    {
        [SerializeField] private Image rightIcon;
        [SerializeField] private Image leftIcon;
        [SerializeField] private Image middleIcon;

        [SerializeField] private Color lightenColor;
        [SerializeField] private Color darkenColor;

        public void DeactiveAll()
        {
            leftIcon.gameObject.SetActive(false);
            middleIcon.gameObject.SetActive(false);
            rightIcon.gameObject.SetActive(false);
        }
        public void ChangeIconSprite(IconPosition position, Sprite sprite)
        {
            if (sprite == null)
                return;
            switch (position)
            {
                case IconPosition.left:
                    leftIcon.sprite = sprite;
                    break;
                case IconPosition.middle:
                    middleIcon.sprite = sprite;
                    break;
                case IconPosition.right:
                    rightIcon.sprite = sprite;
                    break;
            }
        }
        public void ActivateIcon(IconPosition position)
        {
            switch (position)
            {
                case IconPosition.left:
                    leftIcon.gameObject.SetActive(true);
                    break;
                case IconPosition.middle:
                    middleIcon.gameObject.SetActive(true);
                    break;
                case IconPosition.right:
                    rightIcon.gameObject.SetActive(true);
                    break;
            }
        }
        public void DarkenIcon(IconPosition position)
        {
            switch (position)
            {
                case IconPosition.left:
                    leftIcon.color = darkenColor;
                    break;
                case IconPosition.middle:
                    middleIcon.color = darkenColor;
                    break;
                case IconPosition.right:
                    rightIcon.color = darkenColor;
                    break;
            }
        }
        public void LightenIcon(IconPosition position)
        {
            switch (position)
            {
                case IconPosition.left:
                    leftIcon.color = lightenColor;
                    break;
                case IconPosition.middle:
                    middleIcon.color = lightenColor;
                    break;
                case IconPosition.right:
                    rightIcon.color = lightenColor;
                    break;
            }
        }
        public void DeactivateIcon(IconPosition position)
        {
            switch (position)
            {
                case IconPosition.left:
                    leftIcon.gameObject.SetActive(false);
                    break;
                case IconPosition.middle:
                    middleIcon.gameObject.SetActive(false);
                    break;
                case IconPosition.right:
                    rightIcon.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
