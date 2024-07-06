using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class Event 
    {
        public int id;
        public string name;
        [TextArea]
        public string description;
    }
}
