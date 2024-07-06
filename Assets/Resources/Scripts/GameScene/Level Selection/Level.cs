using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	[System.Serializable]
	public class Level
	{
		public int levelIndex;
		public string levelName;
		public string levelDescription;
		public List<ItemData> itemDrops = new List<ItemData>();
		public List<ItemData> itemDropsBronze = new List<ItemData>();
		public List<ItemData> itemDropsSilver = new List<ItemData>();
		public List<ItemData> itemDropsGold = new List<ItemData>();
		public Objective[] objectives = new Objective[3];
		public DialogueGroup dialogue;
		public StageData stageData;
		public bool isUnlocked;
		public List<string> levelUnlockReward = new List<string>();
		public int goldPool;
		public Level(LevelSelectData data)
		{
			this.levelIndex = data.levelIndex;
			this.levelName = data.levelName;
			this.levelDescription = data.levelDescription;
			this.itemDrops = data.itemDrops;
			this.itemDropsBronze = data.itemDropsBronze;
			this.itemDropsSilver = data.itemDropsSilver;
			this.itemDropsGold = data.itemDropsGold;
			this.objectives = data.objectives;
			this.dialogue = data.dialogue;
			this.stageData = data.stageData;
			this.isUnlocked = data.isUnlocked;
			this.levelUnlockReward = data.levelUnlockReward;
			this.goldPool = data.goldPool;
		}
	}
}



