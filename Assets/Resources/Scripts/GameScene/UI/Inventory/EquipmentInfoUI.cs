using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class EquipmentInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private TextMeshProUGUI itemCount;
        [SerializeField] private GameObject useButton;
        [SerializeField] private Image itemIcon;
        public event Action OnUse;
        public void Info(Equipment data)
        {
            if (data != null)
            {
                itemName.text = data.name;
                itemDescription.text = data.description;
                
                //useButton.SetActive(true);
            }
            else
            {
                itemName.text = "";
                itemDescription.text = "";
                itemCount.text = "";
                //useButton.SetActive(false);
            }
        }
        public void Info(Accessories data)
        {
            if (data != null)
            {
                itemName.text = data.name;
                itemDescription.text = data.description;

               // useButton.SetActive(true);
                itemIcon.gameObject.SetActive(true);
                itemIcon.sprite = data.icon;
            }
            else
            {
                itemName.text = "";
                itemDescription.text = "";
                itemCount.text = "";
                //useButton.SetActive(false);
                itemIcon.gameObject.SetActive(false);
                itemIcon.sprite = null;
            }
        }
        public void Use()
        {
            OnUse?.Invoke();
        }
        private void OnDestroy()
        {
            OnUse = null;
        }
    }
}
