using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class Equipment : Item
    {
        public List<SkillType> skillList = new List<SkillType>();
        public Equipment(EquipmentData data) : base(data)
        {
            this.skillList = data.skillList;   
        }
    }
}
