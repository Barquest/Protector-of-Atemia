using UnityEngine;
using DentedPixel;
using System;
using System.Collections;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class PlayerSwordController : Subject , IPausable
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private Animator anim;
        [SerializeField] private float maxSwordRange = 7f;
        [SerializeField] private float travelToTargetSpeed = 7f;
        [SerializeField] private float travelBackSpeed = 7f;
        [SerializeField] private bool isAttacking;
        [SerializeField] private bool isDoneAttacking;
        [SerializeField] private bool attackDelay;
        [SerializeField] private Transform swordParent;


        private bool isPaused;

        private readonly float backDelay = 0.25f;

		public event Action OnBackToPosition;
        public event Action OnStartAttacking;
		public event Action OnAttackSuccess;

		private void Start()
		{
            AddObserver(GameManager.Instance);
		}
		public void Attack(int lane)
        {
            if (!isAttacking && !isDoneAttacking && !attackDelay)
            {
                transform.parent = null;
                LeanTween.moveX(gameObject, lane, 0.25f);
                isAttacking = true;
                isDoneAttacking = false;
                anim.SetBool("Attacking",true);
                OnStartAttacking?.Invoke();
            }
        }
        public void Skill()
        {
            anim.SetTrigger("Skill");
        }
        public void DashRight()
        {
            if (!isDoneAttacking)
            {
                anim.SetTrigger("Right");
            }
        }
        public void DashLeft()
        {
            if (!isDoneAttacking)
            {
                anim.SetTrigger("Left");
            }
        }

        public void Continue()
		{
            isPaused = false;
		}

		public void Pause()
		{
            isPaused = true;
		}

		private void FixedUpdate()
		{
            if (isPaused) return;
            if (isAttacking)
            {
                transform.position += Time.deltaTime * Vector3.forward * travelToTargetSpeed;
                if (transform.position.z > maxSwordRange)
                {
                    DoneAttacking();
                }
            }
            if (isDoneAttacking)
            {
                transform.position = Vector3.MoveTowards(transform.position, swordParent.position, Time.deltaTime * travelBackSpeed);
                if (transform.position.z <= swordParent.position.z)
                {
                    isDoneAttacking = false;
                    transform.position = swordParent.position;
                    transform.parent = swordParent;
                    OnBackToPosition?.Invoke();
                    anim.SetBool("Back", false);
                }
            }
        }
        
        private void DoneAttacking()
        {
            Debug.Log("Done Attacking");
            isAttacking = false;
            anim.SetBool("Attacking", false);
            anim.ResetTrigger("Right");
            anim.ResetTrigger("Left");
            anim.SetBool("Back", true);
            AudioManager.Instance.PlaySfx("Sword Whoosh");
            ParticleScript particle = ObjectPoolController.Instance.particlePool.GetObject();
            particle.transform.position = transform.position;
            StartCoroutine(BackDelay());
        }
        public void AttackCancel()
        {
            Debug.Log("AttackCancel");
            isAttacking = false;
            anim.SetBool("Attacking", false);
            anim.ResetTrigger("Right");
            anim.ResetTrigger("Left");
            anim.SetBool("Back", true);
            StartCoroutine(BackDelay());
        }
        private IEnumerator BackDelay()
        {
            attackDelay = true;
            yield return new WaitForSeconds(backDelay);
            attackDelay = false;
            isDoneAttacking = true;
        }
        private void OnCollisionEnter(Collision collision)
		{

            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (isAttacking)
                {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    if (!enemy.GetIsDead())
                    {
                        AudioManager.Instance.PlaySfx("Sword Hit");
                        OnAttackSuccess?.Invoke();
                        DoneAttacking();
                        ParticleScript blood = ObjectPoolController.Instance.bloodPool.GetObject();
                        blood.transform.position = transform.position;
                        NotifyObservers(Act.Attack);
                        enemy.Damaged();
                    }
                }
            }
            else if (collision.gameObject.CompareTag("Enemy Bullet"))
            {
                Debug.Log("Collision with Bullet");
                if (isAttacking)
                {
                    Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                    bullet.Destroy();
                    AudioManager.Instance.PlaySfx("Sword Hit");
                    OnAttackSuccess?.Invoke();
                    DoneAttacking();
                    NotifyObservers(Act.Attack);
         
                }
            }
		}
		private void OnDestroy()
		{
            OnAttackSuccess = null;
            OnBackToPosition = null;
            OnStartAttacking = null;
		}
	}

}
