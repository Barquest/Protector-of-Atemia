using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class AllyDataScript 
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [TextArea]
        [SerializeField] private string description;
        [SerializeField] private int level;
        [SerializeField] private int currentSkillPoint;
        [SerializeField] private int maxSkillPoint;

        public void AddSkillPoint(int val)
        { 
        
        }
        public void ResetSkillPoint()
        { 
        
        }
        public void ReduceSkillPoint(int val)
        { 
            
        }
    }
}
