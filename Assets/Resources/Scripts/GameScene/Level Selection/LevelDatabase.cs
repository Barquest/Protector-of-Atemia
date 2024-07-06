using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class LevelDatabase : MonoBehaviour
    {
        public static LevelDatabase Instance;

        [SerializeField] private List<LevelSelectData> levelData = new List<LevelSelectData>();

        [SerializeField] private Dictionary<string, LevelSelectData> levelDictionary = new Dictionary<string, LevelSelectData>();

        public LevelSelectData GetLevelData(string value)
        {
            return levelDictionary[value];
        }
        private void Awake()
        {
            Instance = this;
            Initialize();
        }
        private void Initialize()
        {
            levelDictionary.Clear();
            for (int i = 0; i < levelData.Count; i++)
            {
                levelDictionary[levelData[i].levelName] = levelData[i];
            }
        }
        public void ResetData()
        {
            for (int i = 0; i < levelData.Count; i++)
            {
                for (int j = 0; j < levelData[i].objectives.Length; j++)
                {
                    levelData[i].objectives[j].ResetPoint();
                }
#if UNITY_EDITOR
                EditorUtility.SetDirty(levelData[i]);
                AssetDatabase.SaveAssets();
#endif
            }
            Initialize();
        }
    }
}
