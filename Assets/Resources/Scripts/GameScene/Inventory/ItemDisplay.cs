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
        [SerializeField] private TextMeshProUGUI countText;

        public event Action<int> OnClick;
        public void SetData(Item data)
        {
            if (data != null)
            {
                this.data = data;
                Icon.gameObject.SetActive(true);
                Icon.sprite = GlobalGameManager.Instance.ItemDatabase().GetData(data.id).icon;
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
        
    }
}
