using System;
using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public class PlayerShieldController : Subject
	{
		[SerializeField] private Animator anim;
		[SerializeField] private Transform shieldTransform;
		[SerializeField] private GameObject bigShield;

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
		public void Skill()
		{
			anim.SetTrigger("Skill");
		}
		// Start is called before the first frame update
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
					particle.transform.position = shieldTransform.position;
					Parry();
					NotifyObservers(Act.Attack);
					OnAttackSuccess?.Invoke();
					AudioManager.Instance.PlaySfx("Shield Hit");
					GameManager.Instance.ObjectiveProgressing(ObjectiveType.BashEnemy, 1);
				}
			}
			else if (collision.gameObject.CompareTag("Enemy Bullet"))
			{
				collision.gameObject.GetComponent<Bullet>().Destroy();
				ParticleScript particle = ObjectPoolController.Instance.redImpactPool.GetObject();
				particle.transform.position = shieldTransform.position;
				Parry();
				NotifyObservers(Act.Attack);
				OnAttackSuccess?.Invoke();
				AudioManager.Instance.PlaySfx("Shield Hit");
				GameManager.Instance.ObjectiveProgressing(ObjectiveType.BashEnemy, 1);
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
		public void ActivateBigShield()
		{
			bigShield.SetActive(true);
		}
		public void DisactivateBigShield()
		{
			bigShield.SetActive(false);
		}
	}
}
