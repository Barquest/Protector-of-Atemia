using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "LevelSelectData", menuName = "ScriptableObjects/LevelSelectData", order = 1)]
    [SerializeField]
    public class LevelSelectData : ScriptableObject
    {
        public int levelIndex;
        public string levelName;
        public string levelDescription;
        public List<ItemData> itemDrops = new List<ItemData>();
        public List<ItemData> itemDropsBronze = new List<ItemData>();
        public List<ItemData> itemDropsSilver = new List<ItemData>();
        public List<ItemData> itemDropsGold = new List<ItemData>();
        public List<EnemyData> enemies = new List<EnemyData>();
        public Objective[] objectives = new Objective[3];
        public DialogueGroup dialogue;
        public StageData stageData; 
        public bool isUnlocked;
        public List<string> levelUnlockReward = new List<string>();
        public int goldPool;

        public void Save()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}
