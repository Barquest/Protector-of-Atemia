using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected bool isActiveFromStart;

		protected virtual void Awake()
		{
            if (!isActiveFromStart)
            {
                Hide();
            }
            else {
                Show();
            }
		}
		public virtual void Hide()
        {
            canvas.enabled = false;
        }
        public virtual void Show()
        {
            canvas.enabled = true;
        }
    }
}
