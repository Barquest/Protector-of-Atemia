using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class Gift : Item
    {
        public float giftValue;
        public Gift(GiftData data) : base(data)
        {
            this.giftValue = data.giftValue;
        }
    }
}
