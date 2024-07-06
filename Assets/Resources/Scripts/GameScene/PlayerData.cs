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
        public CharacterInventory korvinItems;
        public CharacterInventory aronaItems;
        public float musicVolume = 1;
        public float sfxVolume = 1;
        public Vector3 playerPositionInWorld;

        public void Consume(Item item)
        {
            Item it = Inventory.Find((x) => x == item);
            Consumable consume = (Consumable)it;

            if (consume != null)
            {
                consume.SubtractCount(1);
                if (consume.count <= 0)
                {
                    Inventory.Remove(consume);
                }
            }
        }
        public void Consume(int index)
        {
            Consumable consume = (Consumable)Inventory[index];
            if (consume != null)
            {
                consume.SubtractCount(1);
                if (consume.count <= 0)
                {
                    Inventory.Remove(consume);
                }
            }
        }
        public void AddConsumableToInventory(int id,int count)
        {
            Consumable item = ItemDatabase.Instance.GetConsumable(id,count);
            Inventory.Add(item);
        }
    }
}
