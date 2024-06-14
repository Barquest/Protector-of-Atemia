using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class Item   
    {
        public int id;
        public string name;
        public string description;
        public int count;

        public Item(ItemData data,int count)
        {
            this.id = data.id;
            this.name = data.itemNameIndo;
            this.description = data.descriptionIndo;
            this.count = count;
        }
        public Item(int id, int count)
        {
            ItemData data = GlobalGameManager.Instance.ItemDatabase().GetData(id);
            this.id = data.id;
            this.name = data.itemNameIndo;
            this.description = data.descriptionIndo;
            this.count = count;
        }
        public void Use()
        {
            Debug.Log("Use");
        }
    }
}
