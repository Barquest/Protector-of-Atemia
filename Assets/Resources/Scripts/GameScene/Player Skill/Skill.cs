using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class Skill 
    {
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private SkillType type;
        [SerializeField] private int level;
        [SerializeField] private int currentSkillPoint;
        [SerializeField] private int maxSkillPoint;

        public void AddSkillPoint(int val)
        {
            currentSkillPoint += val;
        }
        public void UseSkill()
        {
            PopupManager.Instance.Display(new PopupDebug("Use" + name + " Level " + level.ToString() + " !!",2f));
            ResetSkillPoint();
        }
        public void ResetSkillPoint()
        {
            currentSkillPoint = 0;
        }
        public int GetCurrentSkillPoint()
        {
            return currentSkillPoint;
        }
        public int GetCurrentMaxSkillPoint()
        {
            return maxSkillPoint;
        }
        public SkillType GetSkillType()
        {
            return type;
        }
    }
}
