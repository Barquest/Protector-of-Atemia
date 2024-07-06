using System.Collections.Generic;
using UnityEngine;
using GleyAllPlatformsSave;
using MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public class GlobalGameManager : MonoBehaviour
	{
		public static GlobalGameManager Instance;

		[SerializeField] private GlobalGameData gameData;
		//[SerializeField] private ItemDatabase itemDatabase;
		[SerializeField] private PlayerData playerData;
		[SerializeField] private Dictionary<int, int> tes;
		public bool isDebug;
		// Start is called before the first frame update

		string savePath;
		bool encrypt = false;
		static string logText = "";
		void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				savePath = Application.persistentDataPath + "/" + "PlayerData";
				CheckLoadGame();
				DontDestroyOnLoad(gameObject);
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}
		}
		private void Start()
		{
			//LanguageManager.Instance.OnLanguageSwitch += SaveLanguageToPlayerData;
			LanguageManager.Instance.SetLanguage(playerData.currentLanguage);
			if (isDebug)
			{
				playerData.Inventory.Add((Accessories)ItemDatabase.Instance.GetAccessories(0));
				playerData.Inventory.Add((Accessories)ItemDatabase.Instance.GetAccessories(1));
				playerData.Inventory.Add((Accessories)ItemDatabase.Instance.GetAccessories(2));
				playerData.Inventory.Add((Accessories)ItemDatabase.Instance.GetAccessories(3));
			}
		}
		public float CurvedYCalculation(float zVal)
		{
			return ( Mathf.Pow(zVal,2)* -0.005f);
		}
		public void CheckLoadGame()
		{
			SaveManager.Instance.Load<PlayerData>(savePath, DataWasLoaded, encrypt);
		}
		public PlayerData GetPlayerData()
		{
			return playerData;
		}
		public void SaveLanguageToPlayerData()
		{
			playerData.currentLanguage = LanguageManager.Instance.GetLanguage();
		}
		public void ResetPlayerData()
		{
			playerData = new PlayerData();
			playerData.unlockedLevelName.Add("Level 1");
			LevelDatabase.Instance.ResetData();
			SaveGame();
		}
		private void DataWasLoaded(PlayerData data, SaveResult result, string message)
		{
			logText += "\nData Was Loaded";
			logText += "\nresult: " + result + ", message: " + message;

			if (result == SaveResult.EmptyData || result == SaveResult.Error)
			{
				logText += "\nNo Data File Found -> Creating new data...";
				playerData = new PlayerData();
			}
			if (result == SaveResult.Success)
			{
				playerData = data;
				AudioManager.Instance.SetMusicVolume(playerData.musicVolume);
				AudioManager.Instance.SetSfxVolume(playerData.sfxVolume);
			}
			Debug.Log(logText);

		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.S))
			{
				SaveGame();
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				ResetPlayerData();
			}
		}
		public void SaveGame()
		{
			SaveManager.Instance.Save(playerData, savePath, DataWasSaved, encrypt);
		}
		private void DataWasSaved(SaveResult result, string message)
		{
			logText = "";
			logText += "\nData Was Saved";
			logText += "\nresult: " + result + ", message: " + message;
			if (result == SaveResult.Error)
			{
				logText += "\nError saving data";
			}
			PopupManager.Instance.Display(new PopupDebug("Data Saved", 2f));
			Debug.Log(logText);
		}
		public void UnlockNewLevel()
		{
			for (int i = 0; i < gameData.levelSelected.levelUnlockReward.Count; i++)
			{
				if (!playerData.unlockedLevelName.Contains(gameData.levelSelected.levelUnlockReward[i]))
				{
					playerData.unlockedLevelName.Add(gameData.levelSelected.levelUnlockReward[i]);
				}
				else
				{
					Debug.Log("Already Unlocked");
				}
			}
		}
		public void SetLevelData(LevelSelectData value)
		{
			gameData.levelSelected = value;
		}
		public LevelSelectData GetLevelData()
		{
			return gameData.levelSelected;
		}
		public List<string> GetUnlockedLevel()
		{
			return playerData.unlockedLevelName;
		}
	}
}
