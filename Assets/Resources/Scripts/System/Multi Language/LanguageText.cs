using UnityEngine;
using TMPro;

namespace MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage
{
    public class LanguageText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private LanguageData data;
        void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }
		private void Start()
		{
            AddToLanguageManager();
            Translate();
        }
		private void OnDestroy()
		{
            RemoveFromLanguageManager();
		}
		private void AddToLanguageManager()
        {
            LanguageManager.Instance.AddText(this);
        }
        private void RemoveFromLanguageManager()
        {
            LanguageManager.Instance.RemoveText(this);
        }
        public void Translate()
        {
            switch (LanguageManager.Instance.GetLanguage())
            {
                case Languages.English:
                    text.text = data.english;
                    break;
                case Languages.Indonesia:
                    text.text = data.indonesia;
                    break;
            }
            
        }
       
    }
}
