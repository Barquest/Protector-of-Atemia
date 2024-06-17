using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public class CameraShake : MonoBehaviour 
	{
		// Transform of the camera to shake. Grabs the gameObject's transform

		// if null.
		public static CameraShake Instance;
		public Transform camTransform;



		// How long the object should shake for.

		public float shakeDuration = 0f;



		// Amplitude of the shake. A larger value shakes the camera harder.

		public float shakeAmount = 0.7f;

		public float decreaseFactor = 1.0f;



		Vector3 originalPos;

		void Awake()

		{
			Instance = this;
			if (camTransform == null)

			{

				camTransform = GetComponent(typeof(Transform)) as Transform;

			}

		}


		public void Shake(float dur, float power)
		{
			shakeDuration = dur;
			shakeAmount = power;
		}
		void OnEnable()

		{

			originalPos = camTransform.localPosition;

		}



		void Update()

		{

			if (shakeDuration > 0)

			{

				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;



				shakeDuration -= Time.deltaTime * decreaseFactor;

			}

			else

			{
				
				shakeDuration = 0f;

				camTransform.localPosition = originalPos;

			}

		}
		public void EarthQuake()
		{
			Shake(3, 1);
		}


	}
}
