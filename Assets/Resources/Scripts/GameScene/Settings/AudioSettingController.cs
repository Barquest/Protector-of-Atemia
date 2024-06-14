using UnityEngine.UI;
using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class AudioSettingController : MonoBehaviour
    {
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;

		public void Setup()
		{
            sfxSlider.value = GlobalGameManager.Instance.GetPlayerData().sfxVolume;
            musicSlider.value = GlobalGameManager.Instance.GetPlayerData().musicVolume;
        }
        public void OnSfxSliderChanged()
        {
            AudioManager.Instance.SetSfxVolume(sfxSlider.value);
            GlobalGameManager.Instance.GetPlayerData().sfxVolume = sfxSlider.value;
        }
        public void OnSfxSliderUp()
        {
            AudioManager.Instance.PlaySfx("Click");
        }
        public void OnMusicSliderChanged()
        {
            AudioManager.Instance.SetMusicVolume(musicSlider.value);
            GlobalGameManager.Instance.GetPlayerData().musicVolume = musicSlider.value;
        }
        public void SaveAudioVolume()
        {
            GlobalGameManager.Instance.GetPlayerData().sfxVolume = sfxSlider.value;
            GlobalGameManager.Instance.GetPlayerData().musicVolume = musicSlider.value;
            Debug.Log("SaveAudioVolume");
        }
    }
}
