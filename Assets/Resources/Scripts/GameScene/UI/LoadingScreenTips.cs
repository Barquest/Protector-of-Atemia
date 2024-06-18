using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class LoadingScreenTips : MonoBehaviour
    {
        [SerializeField] private TipsData tipsData;

        [SerializeField] private TextMeshProUGUI tipsText;
		private void Start()
		{
            SetupTips();
		}
        public void SetupTips()
        {
           
        }
		public void DisplayRandomTips()
        {
            tipsText.text = tipsData.tipsList[Random.Range(0, tipsData.tipsList.Count)];
        }
    }
}
