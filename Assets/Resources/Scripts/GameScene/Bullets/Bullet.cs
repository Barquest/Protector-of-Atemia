using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public enum BulletType 
	{ 
		Sumpit , Arrow
	}
    public class Bullet : MonoBehaviour
    {
		[SerializeField] private BulletType bulletType;
		[SerializeField] private float speed;
		private void FixedUpdate()
		{
			transform.Translate(transform.forward * speed * Time.deltaTime,Space.World);
			if (transform.position.z < -15)
			{
				ReachCaravan();
			}
		}
		public virtual void ReachCaravan()
		{
			GameManager.Instance.DamageCaravan();
			Destroy();
		}

		public void Destroy()
		{
			switch (bulletType)
			{
				case BulletType.Arrow:
					ObjectPoolController.Instance.arrowBulletPool.Push(this);
					break;
				case BulletType.Sumpit:
					ObjectPoolController.Instance.sumpitBulletPool.Push(this);
					break;
			}
		}
	}
}
