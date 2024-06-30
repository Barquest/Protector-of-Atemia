using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class BigShield : MonoBehaviour
    {
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Enemy"))
			{
				Debug.Log("CollisionWith Enemy");
				Enemy enemy = collision.gameObject.GetComponent<Enemy>();
				if (!enemy.GetIsDead())
				{
					enemy.Damaged();
					ParticleScript particle = ObjectPoolController.Instance.redImpactPool.GetObject();
					particle.transform.position = collision.gameObject.transform.position;
				}
			}
			else if (collision.gameObject.CompareTag("Enemy Bullet"))
			{
				collision.gameObject.GetComponent<Bullet>().Destroy();
				ParticleScript particle = ObjectPoolController.Instance.redImpactPool.GetObject();
				particle.transform.position = collision.gameObject.transform.position;
				
			}
		}
	}
}
