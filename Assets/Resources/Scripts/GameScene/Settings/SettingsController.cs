using System;
using UnityEngine;
using TMPro;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;
using UnityEngine.SceneManagement;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class SettingsController : MonoBehaviour
    {
        public static SettingsController Instance;
		[SerializeField] private AudioSettingController audioSettingController; 
        [SerializeField] private Canvas canvas;
		[SerializeField] private GameObject panel;

		public event Action OnClose;
		public event Action OnOpen;
		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}

		}
		private void OnDisable()
		{
			SceneManager.sceneLoaded -= SceneLoaded;
			OnClose -= audioSettingController.SaveAudioVolume;
		}
		private void OnEnable()
		{
			OnClose += audioSettingController.SaveAudioVolume;
			SceneManager.sceneLoaded += SceneLoaded;
		}
		private void SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			Debug.Log("Another Scene is Loaded");
			//AudioManager.Instance
		}
		private void Start()
		{
            Hide();
		}
		public void CloseButton()
		{
			//AudioManager.Instance.PlaySfx("Click");
			OnClose?.Invoke();
		}
		public void ResetAction()
		{
			OnOpen = null;
			OnClose = null;
		}
		public void Show()
        {
			//gameObject.SetActive(true);
			//AudioManager.Instance.PlaySfx("Popup");
			OnOpen?.Invoke();
            canvas.enabled = true;
			panel.SetActive(true);
			audioSettingController.Setup();
        }
        public void Hide()
        {
			//OnClose?.Invoke();
            canvas.enabled = false;
			panel.SetActive(false);
			//gameObject.SetActive(false);
		}
    }
}
