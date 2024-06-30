using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]

    public class CharacterData : ScriptableObject
    {
        public int index;
        public string name;
        [TextArea]
        public string description;
        public GameObject prefab;
    }
}
