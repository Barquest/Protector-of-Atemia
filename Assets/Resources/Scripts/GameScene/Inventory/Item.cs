using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] public int id;
        [SerializeField] public string name;
        [SerializeField] public string description;
        [SerializeField] public Sprite icon;
        [SerializeField] public ItemType itemType;

        public Item(ItemData data)
        {
            this.id = data.id;
            this.name = data.itemName;
            this.description = data.description;
            this.itemType = data.type;
            this.icon = data.icon;
        }
        public Item(int id)
        {
            ItemData data = ItemDatabase.Instance.GetData(id);
            this.id = data.id;
            this.name = data.itemName;
            this.description = data.description;
            this.itemType = data.type;
            this.icon = data.icon;
        }
        public void Use()
        {
            Debug.Log("Use");
        }
    }
}
