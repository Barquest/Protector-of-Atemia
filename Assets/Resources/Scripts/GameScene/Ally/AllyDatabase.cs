using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class AllyDatabase : MonoBehaviour
    {
        public static AllyDatabase Instance;

        [SerializeField] private List<AllyData> allydata = new List<AllyData>();

        [SerializeField] private Dictionary<int, AllyData> allyDictionary = new Dictionary<int, AllyData>();

        public AllyData GetAllyData(int index)
        {
            return allyDictionary[index];
        }
		private void Awake()
		{
            Initialize();
		}
        private void Initialize()
        {
            for (int i = 0; i < allydata.Count; i++)
            {
                allyDictionary[i] = allydata[i];
            }
        }
	}
}
