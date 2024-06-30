using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class PopupDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI text;

        private IEnumerator closeDelayCoroutine;

        public void Show()
        {
            canvas.enabled = true;
        }
        public void Hide()
        {
            canvas.enabled = false;
        }
        public void Popup(Popup data)
        {
            text.text = data.text;
            Show();
        }
        public void PopupDebug(PopupDebug data)
        {
            if (closeDelayCoroutine != null)
                StopCoroutine(closeDelayCoroutine);
            text.text = data.text;
            Show();
            closeDelayCoroutine = CloseDelay(data.delay);
            StartCoroutine(closeDelayCoroutine);
        }
        public void Close()
        {
            Hide();
        }
        private IEnumerator CloseDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Close();
        }
    }
}
