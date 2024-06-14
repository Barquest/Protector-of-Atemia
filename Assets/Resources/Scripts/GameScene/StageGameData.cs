using System;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class StageGameData 
    {
        [SerializeField] private int killCount;
        [SerializeField] private int caravanDamagedCount;

        public event Action<int> OnKillCountChanged;

        public void AddKillCount(int value)
        {
            killCount += value;
            OnKillCountChanged?.Invoke(killCount);
        }
        public void AddDamagedCount(int value)
        {
            caravanDamagedCount += value;
        }
        public int GetDamagedCount()
        {
            return caravanDamagedCount;
        }
    }
}
