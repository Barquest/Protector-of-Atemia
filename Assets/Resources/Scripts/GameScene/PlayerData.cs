using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.MultiLanguage;


namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class PlayerData
    {
        public int money;
        public List<string> unlockedLevelName = new List<string>();
        public Languages currentLanguage;
        public List<Item> Inventory;
        public float musicVolume = 1;
        public float sfxVolume = 1;

        public void Consume(Item item)
        {
            Item consume = Inventory.Find((x) => x == item);
            if (consume != null)
            {
                consume.count--;
                if (consume.count <= 0)
                {
                    Inventory.Remove(consume);
                }
            }
        }
        public void Consume(int index)
        {
            Item consume = Inventory[index];
            if (consume != null)
            {
                consume.count--;
                if (consume.count <= 0)
                {
                    Inventory.Remove(consume);
                }
            }
        }
        public void AddToInventory(int id,int count)
        {
            Item item = GlobalGameManager.Instance.ItemDatabase().Get(id, count);
            Inventory.Add(item);
        }
    }
}
