using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ObjectiveUI : UIController
    {
        [SerializeField] private TextMeshProUGUI[] objectiveText;

		private void Start()
		{
            for (int i = 0; i < objectiveText.Length; i++)
            {
                objectiveText[i].text = "";
            }
		}
		public void WriteObjective(string text, int index)
        {
            objectiveText[index].text = text;
        }
    }
}
