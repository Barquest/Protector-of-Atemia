using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData", order = 1)]
    [SerializeField]
    public class StageData : ScriptableObject
    {
        public List<WaveData> waveList;
    }
}
