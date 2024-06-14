using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;
using TMPro;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public enum SceneEnum
	{
		MainMenu, LevelSelect, Battle
	}
	public class MainMenuManager : UIController
	{
		public static MainMenuManager Instance;

		[SerializeField] private SettingsController settingsController;
		[SerializeField] private InventoryUIController inventoryUIController;
		[SerializeField] private LoadingManager loadingManager;
		[SerializeField] private TextMeshProUGUI moneyText;
		[SerializeField] private SceneEnum currentScene;

		protected override void Awake()
		{
			base.Awake();
			Instance = this;
		}
		protected virtual void Start()
		{
			if (currentScene == SceneEnum.MainMenu)
			{
				SettingsSetup();
				InventorySetup();
				LanguageManager.Instance.OnLanguageSwitch += ShowMoney;
				AudioManager.Instance.PlayMusic("Main Menu Music");

				UpdateUI();

				//List<Item> items = new List<Item>();
				//items.Add(new Item(0, 2));
				//UIPopupController.Instance.AddPopup(new PopupData("Tes Popup", "Tes aja sih ya pokoknya gitu lah yaaa", PopupTesOkay, PopupTesCancel, null));
				//UIPopupController.Instance.AddPopup(new PopupData("HADIAH DARI DEVELOPER", $"SELAMAT KAMU DAPAT {items[0].name} !! sebanyak {items[0].count}", GivePlayerItemId1, items));

			}
		}
		private void PopupTesOkay()
		{
			Debug.Log("PopupTes Okay");
		}
		private void GivePlayerItemId1()
		{
			GlobalGameManager.Instance.GetPlayerData().AddToInventory(0, 2);
		}
		private void PopupTesCancel()
		{
			Debug.Log("Popup Tes Cancel");
		}
		private void OnDestroy()
		{
			LanguageManager.Instance.OnLanguageSwitch -= ShowMoney;
		}
		private void InventorySetup()
		{
			if (inventoryUIController != null)
			{
				inventoryUIController.OnClose += CloseInventory;
			}
		}
		private void SettingsSetup()
		{
			settingsController = SettingsController.Instance;
			settingsController.ResetAction();
			settingsController.OnClose += CloseSettings;
			settingsController.OnClose += Save;
		}
		public void Save()
		{
			GlobalGameManager.Instance.SaveLanguageToPlayerData();
			GlobalGameManager.Instance.SaveGame();
		}
		public void OpenSettings()
		{
			settingsController.Show();
		}
		public void OpenInventory()
		{
			inventoryUIController.Show();
			Hide();
		}
		public void CloseInventory()
		{
			inventoryUIController.Hide();
			Show();
		}
		public void CloseSettings()
		{
			settingsController.Hide();
		}
		private void UpdateUI()
		{
			ShowMoney();
		}
		private void ShowMoney()
		{
			if (moneyText != null)
			{
				moneyText.text = LanguageManager.Instance.GetMainMenuText("money") + GlobalGameManager.Instance.GetPlayerData().money;

			}
		}
		public void LoadLevel(int id)
		{
			loadingManager.LoadScene(id);
		}
	}
}
