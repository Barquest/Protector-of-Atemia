using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class SkillDatabase : MonoBehaviour
    {
        public static SkillDatabase Instance;
		[SerializeField] private List<SkillData> skills = new List<SkillData>();
		[SerializeField] private Dictionary<int,SkillData> skillDictionary;

		public SkillData GetSkillData(int index)
		{
			return skillDictionary[index];
		}
		public void Initialize()
		{
			for (int i = 0; i < skills.Count; i++)
			{
				skillDictionary[i] = skills[i];
			}
		}

		private void Awake()
		{
			Instance = this;
			Initialize();
		}
	
	}
}
