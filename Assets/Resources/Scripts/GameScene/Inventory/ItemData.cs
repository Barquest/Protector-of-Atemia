using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public enum ItemType 
    { 
        Consumable,gift,equipment,accessories
    }
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
    public class ItemData : ScriptableObject
    {
        public int id;
        public string itemName;
        [TextArea]
        public string description;
        public Sprite icon;
        public ItemType type;
    }
}
