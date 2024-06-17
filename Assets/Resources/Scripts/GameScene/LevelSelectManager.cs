using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class LevelSelectManager : MonoBehaviour
    {
        [SerializeField] private Transform levelSelectParent;
        [SerializeField] private List<LevelSelectButton> levelList;
		[SerializeField] private List<LevelSelectData> levelData;
		[SerializeField] private LevelSelectUI levelSelectUI;
		[SerializeField] private LevelSelectCharacter levelSelectCharacter;

		[SerializeField] private Dictionary<string, LevelSelectButton> levelDictionaries = new Dictionary<string, LevelSelectButton>();
		[SerializeField] private bool isWalking;


		private LevelSelectButton levelSelected;
		private LevelSelectButton lastLevelSelected;
		private int currentIndex = 0;
		private void Start()
		{
			LeanTween.reset();
			levelSelectCharacter.OnWalkOver += WalkOver;
			SetupLevelList();
			CheckLevelAvailable();
			currentIndex = 0;
			//DisplayLevel(levelList[0]);
		}
		private void SetupLevelList()
		{
			levelList.Clear();
			lastLevelSelected = null;
			foreach (Transform t in levelSelectParent)
			{
				levelList.Add(t.GetComponent<LevelSelectButton>());
			}
			for (int i = 0; i < levelList.Count; i++)
			{
				levelList[i].Setup();
				levelList[i].SetIndex(i);
				levelList[i].SetLevel(levelData[i]);
				levelList[i].OnClick += DisplayLevel;
				levelDictionaries[levelData[i].levelName] = levelList[i];
			}
		}
		private void WalkOver()
		{
			isWalking = false;
			levelSelectUI.Show();
			levelSelectCharacter.SetAnimWalk(false);
		}
		public void DisplayLevel(LevelSelectButton data)
		{
			/*
			if (!isWalking)
			{
				if (levelSelected == data)
				{
					Debug.Log("Same");
					return;
				}
				isWalking = true;

				List<Vector3> targets = new List<Vector3>();
				if (currentIndex < data.GetIndex())
				{
					for (int i = currentIndex+1; i <= data.GetIndex(); i++)
					{
						targets.Add(levelList[i].transform.position);
					}
					levelSelectCharacter.Walks(targets);
					levelSelectCharacter.SetAnimWalk(true);
					levelSelectUI.Hide();
				}
				else if (currentIndex > data.GetIndex())
				{
					for (int i = currentIndex-1; i >= data.GetIndex(); i--)
					{
						targets.Add(levelList[i].transform.position);
					}
					levelSelectCharacter.Walks(targets);
					levelSelectCharacter.SetAnimWalk(true);
					levelSelectUI.Hide();
				}
				else {
					if (data.GetIndex() == 0)
					{
						Debug.Log("Walkto");
						levelSelectCharacter.WalkTo(data.transform.position);
						levelSelectCharacter.SetAnimWalk(true);
						levelSelectUI.Hide();
					}

				}


				if (data.isLocked)
				{
					levelSelectUI.DisplayData(data.GetLevelData());
					levelSelectUI.DisplayLocked();
				}
				else
				{
					levelSelectUI.DisplayData(data.GetLevelData());
				}
				lastLevelSelected = levelSelected;
				levelSelected = data;
				currentIndex = data.GetIndex();
			}*/
			Debug.Log("DisplayData");
			if (data.isLocked)
			{
				Debug.Log("Data is Locked");
				levelSelectUI.DisplayData(data.GetLevelData());
				levelSelectUI.DisplayLocked();
			}
			else
			{
				Debug.Log("Data is Unlocked");
				levelSelectUI.DisplayData(data.GetLevelData());
			}
			//lastLevelSelected = levelSelected;
			levelSelected = data;
			currentIndex = data.GetIndex();
		}
		public void PlayLevel()
		{
			if (levelSelected.isLocked)
			{
				Debug.Log("Locked!!");
			}
			else
			{
				GlobalGameManager.Instance.SetLevelData(levelSelected.GetLevelData());
				MainMenuManager.Instance.LoadLevel(3);
			}
		}
		public void Hide()
		{
			levelSelectUI.Hide();
		}
		private void CheckLevelAvailable()
		{
			Debug.Log("CheckLevelAvailable");

			List<string> levelUnlocked = GlobalGameManager.Instance.GetUnlockedLevel();
			for (int i = 0; i < levelUnlocked.Count; i++)
			{
				Debug.Log("Unlocking");
				levelDictionaries[levelUnlocked[i]].Unlock();
			}
		}
	}
	
}
