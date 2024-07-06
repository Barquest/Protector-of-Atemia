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
        [SerializeField] private TextMeshProUGUI[] objectivesText;
        [SerializeField] private GameObject[] objectivesCheck;
        [SerializeField] private Transform itemRewardParent;

        [SerializeField] private GameObject unlocked;
        [SerializeField] private GameObject locked;

        public void DisplayData(LevelSelectData data)
        {
            Show();
            locked.SetActive(false);
            levelSelected = data;
            levelName.text = data.levelName;
            levelDescription.text = data.levelDescription;
            foreach (Transform t in itemRewardParent)
            {
                Destroy(t.gameObject);
            }
            int gold = data.goldPool;
            ItemDisplay itd = Instantiate(itemDisplayPrefab, itemRewardParent);
            itd.SetData(gold);
            if (data.objectives[0].GetName() != "")
            {
                Objective obj = data.objectives[0];
                objectivesText[0].text = obj.GetName();
                if (obj.IsIncremental())
                {
                    objectivesText[0].text += "(" + obj.GetCurPoint() + " / " + obj.GetMaxPoint() + ")";
                }
                objectivesCheck[0].SetActive(obj.IsCompleted());
            }
            else {
                objectivesText[0].text = "";
            }
            if (data.objectives[1].GetName() != "")
            {
                Objective obj = data.objectives[1];
                objectivesText[1].text = obj.GetName();
                if (obj.IsIncremental())
                {
                    objectivesText[1].text += "(" + obj.GetCurPoint() + " / " + obj.GetMaxPoint() + ")";
                }
                objectivesCheck[1].SetActive(obj.IsCompleted());

            }
            else {
                objectivesText[1].text = "";
            }
            if (data.objectives[2].GetName() != "")
            {
                Objective obj = data.objectives[2];
                objectivesText[2].text = obj.GetName();
                if (obj.IsIncremental())
                {
                    objectivesText[2].text += "(" + obj.GetCurPoint() + " / " + obj.GetMaxPoint() + ")";
                }
                objectivesCheck[2].SetActive(obj.IsCompleted());
            }
            else
            {
                objectivesText[2].text = "";
            }
            if (data.itemDrops.Count > 0)
            {
                for (int i = 0; i < data.itemDrops.Count; i++)
                {
                    ItemDisplay display = Instantiate(itemDisplayPrefab, itemRewardParent);
                    Item it= ItemDatabase.Instance.GetItem(data.itemDrops[i].id);
                    display.SetData(it);
                }
            }
            if (data.itemDropsBronze.Count > 0)
            {
                for (int i = 0; i < data.itemDropsBronze.Count; i++)
                {
                    ItemDisplay display = Instantiate(itemDisplayPrefab, itemRewardParent);
                    Item it = ItemDatabase.Instance.GetItem(data.itemDropsBronze[i].id);
                    display.SetData(it,ObjectiveReward.Bronze);
                    if (data.objectives[0].GetName() != "")
                    {
                        display.CheckList(data.objectives[0].IsCompleted());
                    }
                }
            }
            if (data.itemDropsSilver.Count > 0)
            {
                for (int i = 0; i < data.itemDropsSilver.Count; i++)
                {
                    ItemDisplay display = Instantiate(itemDisplayPrefab, itemRewardParent);
                    Item it = ItemDatabase.Instance.GetItem(data.itemDropsSilver[i].id);
                    display.SetData(it, ObjectiveReward.Silver);
                    if (data.objectives[1].GetName() != "")
                    {
                        display.CheckList(data.objectives[1].IsCompleted());
                    }
                }
            }
            if (data.itemDropsGold.Count > 0)
            {
                for (int i = 0; i < data.itemDropsGold.Count; i++)
                {
                    ItemDisplay display = Instantiate(itemDisplayPrefab, itemRewardParent);
                    Item it = ItemDatabase.Instance.GetItem(data.itemDropsGold[i].id);
                    display.SetData(it, ObjectiveReward.Gold);
                    if (data.objectives[2].GetName() != "")
                    {
                        display.CheckList(data.objectives[2].IsCompleted());
                    }
                }
            }

        }
        public void DisplayLocked()
        {
            locked.SetActive(true);
        }
    }
}
