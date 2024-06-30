using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "EquipmentData", menuName = "ScriptableObjects/EquipmentData", order = 1)]
    public class EquipmentData : ItemData
    {
        public List<SkillType> skillList = new List<SkillType>();
        public CharacterEnum characterCanEquip;
    }
}
