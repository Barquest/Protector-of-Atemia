using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public enum ItemType 
    { 
        Consumable   
    }
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
    public class ItemData : ScriptableObject
    {
        public int id;
        public string itemNameIndo;
        public string itemNameEnglish;
        public string descriptionIndo;
        public string descriptionEnglish;
        public Sprite icon;
    
    }
}
