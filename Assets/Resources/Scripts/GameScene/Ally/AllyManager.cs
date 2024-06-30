using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class AllyManager : MonoBehaviour
    {
        [SerializeField] private AllyData[] allyData = new AllyData[6];
        [SerializeField] private Ally[] allyObject = new Ally[6];
        [SerializeField] private Transform[] allyTransform = new Transform[6];

        public void AddAllySkillPoint()
        { 
        
        }
        public void UseAllySkill(int index)
        { 
        
        }
        public Ally GetAlly(int index)
        {
            return allyObject[index];
        }
        public void SetAlly(int index, AllyData ally)
        {
            allyData[index] = ally;
        }
      //  [SerializeField] private ally[] allyData = new AllyData[6];
    }
}
