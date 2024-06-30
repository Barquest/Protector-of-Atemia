using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class PopupManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static PopupManager Instance;
        [SerializeField] private PopupDisplay display;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
        public void Display(Popup data)
        {
            display.Popup(data);
        }
        public void Display(PopupDebug data)
        {
            display.PopupDebug(data);
        }
    }
}
