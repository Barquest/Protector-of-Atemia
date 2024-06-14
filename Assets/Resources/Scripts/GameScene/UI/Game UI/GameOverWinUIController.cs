using TMPro;
using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class GameOverWinUIController : UIController
    {
        [SerializeField] private TextMeshProUGUI winText;
        [SerializeField] private TextMeshProUGUI rielRewardText;
        [SerializeField] private GameObject rielRewardPanel;
        public virtual void ShowWin(int rielReward)
        {
            winText.text = LanguageManager.Instance.GetGameplayText("youwin");
            rielRewardText.text = LanguageManager.Instance.GetGameplayText("rieldrop") + " : " + rielReward.ToString();
            rielRewardPanel.SetActive(true);
            canvas.enabled = true;
        }
        public virtual void ShowLose()
        {
            winText.text = LanguageManager.Instance.GetGameplayText("youlose");
            rielRewardText.text = "";
            rielRewardPanel.SetActive(false);
            canvas.enabled = true;
        }
    }
}
