using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;
using UnityEngine.UI;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class OptionController : MonoBehaviour
    {
        [SerializeField] private AudioManager soundManager;

		private void Start()
		{
            soundManager = AudioManager.Instance;
		}
		public void SetSfxSoundVolume(float value)
        {
            soundManager.SetSfxVolume(value);
        }
        public void SetMusicSoundVolume(float value)
        {
            soundManager.SetMusicVolume(value);
        }
    }
}
