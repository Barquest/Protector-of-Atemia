using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "LevelSelectData", menuName = "ScriptableObjects/LevelSelectData", order = 1)]
    [SerializeField]
    public class LevelSelectData : ScriptableObject
    {
        public int levelIndex;
        public string levelName;
        public string levelDescription;
        public List<Item> itemDrops;
        public List<Item> itemDropsPerfect;
        public StageData stageData; 
        public bool isUnlocked;
        public string levelUnlockReward;
        public int goldPool;
    }
}
