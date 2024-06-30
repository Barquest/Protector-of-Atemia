using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "AccessoriesData", menuName = "ScriptableObjects/AccessoriesData", order = 1)]
    public class AccessoriesData : ItemData
    {
        public List<PassiveSkillType> skillList = new List<PassiveSkillType>();
        public CharacterEnum characterCanEquip;

    }
}
