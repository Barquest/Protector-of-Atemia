using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public interface IObserver
    {
        public void OnNotify(Act act);
    }
}
