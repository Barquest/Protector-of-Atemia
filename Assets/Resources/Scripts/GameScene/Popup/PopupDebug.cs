using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class PopupDebug : Popup
    {
        [SerializeField] public float delay { get; protected set; }

        public PopupDebug(string text,float delay) : base (text)
        {
            this.text = text;
            this.delay = delay;

        }
    }
}
