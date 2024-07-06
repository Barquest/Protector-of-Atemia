using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObjects/EventData", order = 1)]

    public class EventData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [TextArea]
        [SerializeField] private string description;
    }
}
