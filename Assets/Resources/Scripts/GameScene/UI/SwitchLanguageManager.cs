using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class SwitchLanguageManager : MonoBehaviour
    {
        [SerializeField] private SwitchLanguageButton[] buttons;
		[SerializeField] private SwitchLanguageButton currentlySelected;
		
		private void OnEnable()
		{
			for (int i = 0; i < buttons.Length; i++)
			{
				if (buttons[i].GetLanguage() == LanguageManager.Instance.GetLanguage())
				{
					buttons[i].Select();
					currentlySelected = buttons[i];
				}
				else
				{
					buttons[i].UnSelect();
				}
			}
		}
		public void SelectLanguage(Languages language)
		{
			currentlySelected.UnSelect();
			AudioManager.Instance.PlaySfx("Click");
			for (int i = 0; i < buttons.Length; i++)
			{
				if (buttons[i].GetLanguage() == language)
				{
					buttons[i].Select();
					currentlySelected = buttons[i];
					LanguageManager.Instance.ChangeLanguage(language);
				}
			}
		}
	}
}
