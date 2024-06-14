using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
namespace MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage
{
    public enum Languages
    { 
        English,Indonesia
    }

    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager Instance;
        [SerializeField] private Languages currentLanguage;
        [SerializeField] private List<LanguageData> MainMenuDynamicText;
        [SerializeField] private List<LanguageData> GameplayDynamicText;
        [SerializeField] private List<LanguageText> TextInScene;

        public event Action OnLanguageSwitch;
        // Start is called before the first frame update
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                if (Instance != this)
                    Destroy(gameObject);
            }

        }
        private bool active = false;
        /*public void ChangeLocale(int localeID)
        {
            if (active == true)
                return;
            StartCoroutine(SetLocale(localeID));
        }
        private IEnumerator SetLocale(int _localeID)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
            active = false;
        }*/
        public void SwitchLanguage()
        {
            if (currentLanguage == Languages.Indonesia)
            {
                currentLanguage = Languages.English;
            }
            else
            {
                currentLanguage = Languages.Indonesia;
            }
            foreach (LanguageText t in TextInScene)
            {
                t.Translate();
            }
            OnLanguageSwitch?.Invoke();
        }
        public void ChangeLanguage(Languages language)
		{
            Debug.Log("Changing Language to " + language.ToString());
            currentLanguage = language;
            
            foreach (LanguageText t in TextInScene)
            {
                t.Translate();
            }
        }
        public void SetLanguage(Languages language)
        {
            Debug.Log("Set Language to " + language.ToString());
            currentLanguage = language;
        }
        public void AddText(LanguageText text)
        {
            TextInScene.Add(text);
            text.Translate();
        }
        public void RemoveText(LanguageText text)
        {
            TextInScene.Remove(text);
        }
        public string GetMainMenuText(string id)
        {
            LanguageData text = MainMenuDynamicText.Find((x) => x.id == id);
            string data = "";
            if (currentLanguage == Languages.English)
            {
                data = text.english;
            }
            else if (currentLanguage == Languages.Indonesia)
            {
                data = text.indonesia;
            }
            return data;
        }
        public string GetGameplayText(string id)
        {
            LanguageData text = GameplayDynamicText.Find((x) => x.id == id);
            string data = "";
            if (currentLanguage == Languages.English)
            {
                data = text.english;
            }
            else if (currentLanguage == Languages.Indonesia)
            {
                data = text.indonesia;
            }
            return data;
        }
        public Languages GetLanguage()
        {
            return currentLanguage;
        }
    }
}
