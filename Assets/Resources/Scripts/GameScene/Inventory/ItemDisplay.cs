using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private Item data;
        [SerializeField] private Image Icon;
        [SerializeField] private Image Background;
        [SerializeField] private GameObject checkListObject;
        [SerializeField] private TextMeshProUGUI countText;


        [SerializeField] private Sprite goldIcon;
        [SerializeField] private Sprite defaultBg;
        [SerializeField] private Sprite bronzeBg;
        [SerializeField] private Sprite silverBg;
        [SerializeField] private Sprite goldBg;

        public event Action<int> OnClick;

        public void SetData(int gold)
        {
            Icon.sprite = goldIcon;
            Icon.gameObject.SetActive(true);
            Background.sprite = defaultBg;
            countText.text = gold.ToString();
        }
        public void CheckList(bool val)
        {
            checkListObject.gameObject.SetActive(val);
        }
        public void SetData(Item data)
        {
            if (data != null)
            {
                this.data = data;
                Icon.gameObject.SetActive(true);
                Icon.sprite = ItemDatabase.Instance.GetData(data.id).icon;
                Background.sprite = defaultBg;
                countText.text = "";
            }
        }
        public void SetIcon(Sprite data)
        {
            if (data != null)
            {
                Icon.gameObject.SetActive(true);
                Icon.sprite = data;
                Background.sprite = defaultBg;
                countText.text = "";
            }
        }
        public void SetData(Item data,ObjectiveReward reward)
        {
            if (data != null)
            {
                this.data = data;
                Icon.gameObject.SetActive(true);
                Icon.sprite = ItemDatabase.Instance.GetData(data.id).icon;
                Background.sprite = defaultBg;
                countText.text = "";
                if (reward == ObjectiveReward.Bronze)
                {
                    Background.sprite = bronzeBg;
                }
                else if (reward == ObjectiveReward.Silver)
                {
                    Background.sprite = silverBg;
                }
                else {
                    Background.sprite = goldBg;
                }
            }
        }

        public void SetData(Consumable data)
        {
            if (data != null)
            {
                this.data = data;
                Icon.gameObject.SetActive(true);
                Icon.sprite = ItemDatabase.Instance.GetData(data.id).icon;
                countText.text = data.count.ToString();
            }
        }
        public Item GetData()
        {
            return data;
        }
        public void SetEmptyData(int index)
        {
            this.index = index;
            data = null;
            Icon.gameObject.SetActive(false);
            countText.text = "";
        }
        public void DisplaySelect()
        {
            Background.color = Color.green;
        }
        public void DisplayUnselect()
        {
            Background.color = Color.white;
        }
        public void Click()
        {
            OnClick?.Invoke(index);
        }
        public void SetEquipped()
        {
            countText.text = "E";
        }
        public void SetCountEmpty()
        {
            countText.text = "";
        }
        
    }
}
