using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    [System.Serializable]
    public class Popup 
    {
        [SerializeField] public virtual string text { get; protected set; }

        public Popup(string text)
        {
            this.text = text;
        }
      
    }
}
