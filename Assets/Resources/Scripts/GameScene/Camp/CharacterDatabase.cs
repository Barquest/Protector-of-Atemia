using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CharacterDatabase : MonoBehaviour
    {
        public static CharacterDatabase Instance;

        [SerializeField] private List<CharacterData> charData = new List<CharacterData>();

        [SerializeField] private Dictionary<int, CharacterData> charDictionary = new Dictionary<int, CharacterData>();

        public CharacterData GetCharacterData(int index)
        {
            return charDictionary[index];
        }
        private void Awake()
        {
            Instance = this;
            Initialize();
        }
        private void Initialize()
        {
            for (int i = 0; i < charData.Count; i++)
            {
                charDictionary[i] = charData[i];
            }
        }
    }
}
