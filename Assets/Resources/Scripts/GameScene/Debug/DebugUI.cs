using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class DebugUI : UIController
    {
        public void Close()
        {
            Hide();
        }
        public void ResetData()
        {
            GlobalGameManager.Instance.ResetPlayerData();
            MainMenuManager.Instance.LoadLevel(1);
        }
    }
}
