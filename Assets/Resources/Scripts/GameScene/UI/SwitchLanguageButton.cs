using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class SwitchLanguageButton : MonoBehaviour
    {
        [SerializeField] private GameObject checkObject;
        // Start is called before the first frame update
        [SerializeField] private Image buttonImage;
        [SerializeField] private Color unselectedColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Languages language;
        [SerializeField] private SwitchLanguageManager switchLanguageManager;

        public Languages GetLanguage()
        {
            return language;
        }
        public void Click()
        {
            switchLanguageManager.SelectLanguage(language);
        }
        public void Select()
        {
            buttonImage.color = selectedColor;
            checkObject.SetActive(true);
        }
        public void UnSelect()
        {
            buttonImage.color = unselectedColor;
            checkObject.SetActive(false);
        }
    }
}
