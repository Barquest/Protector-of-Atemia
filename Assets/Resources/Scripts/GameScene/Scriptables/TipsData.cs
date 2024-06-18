using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "TipsData", menuName = "ScriptableObjects/TipsData", order = 1)]
    [SerializeField]
    public class TipsData : ScriptableObject
    {
        [TextArea]
        public List<string> tipsList;
    }
}
