using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public enum ObjectiveReward
    { 
        Bronze,Silver,Gold
    }
    public enum ObjectiveType
    { 
        killEnemy,SlashEenemy,BashEnemy,useSkill
    }
    [System.Serializable]
    public class Objective
    {
        [SerializeField] private string name;
        [SerializeField] private ObjectiveType objective;
        [SerializeField] private int curPoint;
        [SerializeField] private int maxPoint;
        [SerializeField] private bool isIncremental;
        [SerializeField] private bool isCompleted;

        public void ResetPoint()
        {
            curPoint = 0;
            isCompleted = false;
        }
        public string GetName()
        {
            return name;
        }
        public ObjectiveType GetType()
        {
            return objective;
        }
        public void Progressing(int val)
        {
            if (isIncremental)
            {
                curPoint += val;
                if (curPoint >= maxPoint)
                {
                    curPoint = maxPoint;
                    Completed();
                }
            }
            else {
                Completed();
            }
        }
        public int GetCurPoint()
        {
            return curPoint;
        }
        public int GetMaxPoint()
        {
            return maxPoint;
        }
        public bool IsIncremental()
        {
            return isIncremental;
        }
        public bool IsCompleted()
        {
            return isCompleted;
        }
        public void Completed()
        {
            isCompleted = true;
        }
    }
}
