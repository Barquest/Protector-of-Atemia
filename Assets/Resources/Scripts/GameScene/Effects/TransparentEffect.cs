using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class TransparentEffect : MonoBehaviour
    {
        [SerializeField] private Material[] mat;
        [SerializeField] private Color color;
        [SerializeField] private float currentAlpha;
        [SerializeField] private float fadingSpeed;
        [SerializeField] private float fadeDelay;
        // Start is called before the first frame update
        void Start()
        {
            ResetAlpha();
            StartDecaying();
        }
        public void ResetAlpha()
        {
            currentAlpha = 100;
            color.a = currentAlpha / 100;
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i].color = new Color(mat[i].color.r, mat[i].color.g, mat[i].color.b, color.a);
            }
        }
        private void SetColor(Color color)
        { 
        
        }

        public void StartDecaying()
        {
            StartCoroutine(SlowlyDisapearing());
        }
        private IEnumerator SlowlyDisapearing()
        {
            Debug.Log("StartSlowlyDisapearing");
            yield return new WaitForSeconds(fadeDelay);
            Debug.Log("SlowlyDisapearing");
            while (currentAlpha > 0)
            {
                currentAlpha -= Time.deltaTime * fadingSpeed;
                color.a = currentAlpha / 100;
                // mat.SetColor("_Color", color);
                for (int i = 0; i < mat.Length; i++)
                {
                    mat[i].color = new Color(mat[i].color.r, mat[i].color.g, mat[i].color.b, color.a);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
