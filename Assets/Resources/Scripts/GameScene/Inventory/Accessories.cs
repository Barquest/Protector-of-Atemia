using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class Accessories : Item
    {
        public List<PassiveSkillType> skillList = new List<PassiveSkillType>();
        public CharacterEnum characterUse;

        public Accessories(AccessoriesData data) : base(data)
        {
            this.skillList = data.skillList;
            this.characterUse = data.characterCanEquip;
            
        }
    }
}
