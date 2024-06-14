using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class PauseMenuUIController : UIController
    {
        public void ContinueGame()
        {
            GameManager.Instance.ContinueGame();
        }
    }
}
