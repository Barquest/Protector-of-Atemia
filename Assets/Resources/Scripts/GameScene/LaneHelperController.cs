using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class LaneHelperController : MonoBehaviour
    {
        [SerializeField] private Material mat1;
        [SerializeField] private Material mat2;
		[SerializeField] private Color color1;
		[SerializeField] private Color color2;
		[SerializeField] private float currentAlpha;
		[SerializeField] private float fadingSpeed;
		[SerializeField] private float fadeDelay;
		[SerializeField] private GameObject laneHelper;


		private void Start()
		{
			//Shader shader;
			//shader = Shader.Find("Curved Transparent");
			Hide();
		}
		public void StartDecaying()
		{
			Show();
			currentAlpha = 40;
			color1.a = currentAlpha / 100;
			color2.a = currentAlpha / 100;
			mat1.SetColor("_Color", color1);
			mat2.SetColor("_Color", color2);
			StartCoroutine(SlowlyDisapearing());
		}
		public void Show()
		{
			laneHelper.SetActive(true);
		}
		public void Hide()
		{
			laneHelper.SetActive(false);
		}
		private IEnumerator SlowlyDisapearing()
		{
			yield return new WaitForSeconds(fadeDelay);
			while (currentAlpha > 0)
			{
				currentAlpha -= Time.deltaTime * fadingSpeed;
				color1.a = currentAlpha/100;
				color2.a = currentAlpha/100;
				mat1.SetColor("_Color", color1);
				mat2.SetColor("_Color", color2);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
