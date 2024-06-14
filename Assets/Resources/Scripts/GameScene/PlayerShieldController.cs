using System;
using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public class PlayerShieldController : Subject
	{
		[SerializeField] private Animator anim;
		[SerializeField] private Transform shieldTransform;

		public event Action OnAttackSuccess;
		private void Start()
		{
			AddObserver(GameManager.Instance);
		}
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				Parry();
			}
		}
		// Start is called before the first frame update
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Enemy"))
			{
				Debug.Log("CollisionWith Enemy");
				collision.gameObject.GetComponent<Enemy>().Damaged();
				ParticleScript particle = ObjectPoolController.Instance.redImpactPool.GetObject();
				particle.transform.position = shieldTransform.position;
				Parry();
				NotifyObservers(Act.Attack);
				OnAttackSuccess?.Invoke();
				AudioManager.Instance.PlaySfx("Shield Hit");
			}
		}
		private void Parry()
		{
			anim.SetTrigger("Parry");
		}
		public void DashRight()
		{
			//if (!isDoneAttacking)
			//{
				anim.SetTrigger("Right");
			//}
		}
		public void DashLeft()
		{
			//if (!isDoneAttacking)
			//{
				anim.SetTrigger("Left");
			//}
		}
	}
}
