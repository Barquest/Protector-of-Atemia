using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 1)]
    [SerializeField]
    public class WaveData : ScriptableObject
    {
        public List<WaveUnitData> units;
        [Tooltip("Set 0 to no Force Next wave")]
        public float ForceNextWaveDelay;
    }
}
