using TMPro;
using UnityEngine;
namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class LevelSelectUI : UIController
    {
        [SerializeField] private LevelSelectData levelSelected;
        [SerializeField] private ItemDisplay itemDisplayPrefab;
        [SerializeField] private TextMeshProUGUI levelName;
        [SerializeField] private TextMeshProUGUI levelDescription;
        [SerializeField] private TextMeshProUGUI goldReward;
        [SerializeField] private Transform itemRewardParent;
        [SerializeField] private Transform itemPerfectRewardParent;

        [SerializeField] private GameObject unlocked;
        [SerializeField] private GameObject locked;

        public void DisplayData(LevelSelectData data)
        {
            locked.SetActive(false);
            levelSelected = data;
            levelName.text = data.levelName;
            levelDescription.text = data.levelDescription;
            goldReward.text = data.goldPool.ToString();
            foreach (Transform t in itemRewardParent)
            {
                Destroy(t.gameObject);
            }
            if (data.itemDrops.Count > 0)
            {
                for (int i = 0; i < data.itemDrops.Count; i++)
                {
                    ItemDisplay display = Instantiate(itemDisplayPrefab, itemRewardParent);
                    display.SetData(data.itemDrops[i]);
                }
            }
            foreach (Transform t in itemPerfectRewardParent)
            {
                Destroy(t.gameObject);
            }
            if (data.itemDropsPerfect.Count > 0)
            {
                for (int i = 0; i < data.itemDropsPerfect.Count; i++)
                {
                    ItemDisplay display = Instantiate(itemDisplayPrefab, itemPerfectRewardParent);
                    display.SetData(data.itemDropsPerfect[i]);
                }
            }
        }
        public void DisplayLocked()
        {
            locked.SetActive(true);
        }
    }
}
