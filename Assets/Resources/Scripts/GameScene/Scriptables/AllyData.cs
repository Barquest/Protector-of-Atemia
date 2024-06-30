using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{ 
    [CreateAssetMenu(fileName = "AllyData", menuName = "ScriptableObjects/AllyData", order = 1)]

    public class AllyData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [TextArea]
        [SerializeField] private string description;
        [SerializeField] private GameObject allyPrefab;
    }
}
