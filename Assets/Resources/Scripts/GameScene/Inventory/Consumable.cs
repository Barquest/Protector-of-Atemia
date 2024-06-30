using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class Consumable : Item
    {
        [SerializeField] public int count { get; private set; }
        public Consumable(ItemData data,int count) : base(data)
        {
            this.count = count;
        }
        public void SubtractCount(int val)
        {
            count -= val;
        }
    }
}
