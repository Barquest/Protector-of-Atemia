using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ParticleScript : MonoBehaviour
    {
		[SerializeField] private float disableDelay;
		[SerializeField] private ParticleSystem particle;
		private void OnEnable()
		{
			if (particle != null)
			{
				particle?.Play();
			}
			StartCoroutine(DisableDelay());
		}
		private IEnumerator DisableDelay()
		{
			yield return new WaitForSeconds(disableDelay);
			Disablethis();
		}
		protected virtual void Disablethis()
		{
			ObjectPoolController.Instance.particlePool.Push(this);
		}
	}
}
