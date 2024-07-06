using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ObjectiveManager : MonoBehaviour
    {

        [SerializeField] private Objective[] objective = new Objective[3];

        [SerializeField] private Dictionary<string, Objective> objectiveDictionary = new Dictionary<string, Objective>();
        [SerializeField] private List<ObjectiveType> typeList = new List<ObjectiveType>();
        [SerializeField] private ObjectiveUI objectiveUI;

        private void InitializeDictionary()
        {
            objectiveDictionary.Clear();
            typeList.Clear();
            for (int i = 0; i < objective.Length; i++)
            {
                objectiveDictionary[objective[i].GetName()] = objective[i];
                typeList.Add(objective[i].GetType());
                string text = objective[i].GetName() + "(" + objective[i].GetCurPoint() + "/" + objective[i].GetMaxPoint() + ")";
                if (objective[i].IsCompleted())
                {
                    objectiveUI.WriteObjective("<s>" + text + "</s>", i);
                }
                else
                {
                    objectiveUI.WriteObjective(text, i);
                }
            }
        }
        public void HideUI()
        {
            objectiveUI.Hide();
        }
        public void ShowUI()
        {
            objectiveUI.Show();
        }
        public void SetObjectiveData(Objective[] val)
        {
            objective = val;
            InitializeDictionary();
        }
        public void ProgressObjective(ObjectiveType type,int val)
        {
            if (!typeList.Contains(type)) return;
            for (int i = 0; i < objective.Length; i++)
            {
                if (objective[i].GetType() == type)
                {
                    objective[i].Progressing(val);
                    string text = objective[i].GetName() + "(" + objective[i].GetCurPoint() + "/" + objective[i].GetMaxPoint() + ")";
                    if (objective[i].IsCompleted())
                    {
                        objectiveUI.WriteObjective("<s>"+text+"</s>", i);
                    }
                    else
                    {
                        objectiveUI.WriteObjective(text, i);
                    }
                }
            }
        }
        public void SaveToPlayerData()
        {
#if UNITY_EDITOR
            LevelSelectData data = GlobalGameManager.Instance.GetLevelData();
            data.objectives = objective;
            data.Save();
#endif
        }
    }
}
