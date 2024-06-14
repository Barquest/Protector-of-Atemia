using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage
{
    [CreateAssetMenu(fileName = "LanguageData", menuName = "ScriptableObjects/LanguageData", order = 1)]

    public class LanguageData : ScriptableObject
    {
        public string id;
        public string english;
        public string indonesia;
    }
}