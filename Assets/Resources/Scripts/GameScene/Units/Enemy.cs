using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class Enemy : MonoBehaviour , IPausable
    {
        [SerializeField] protected EnemyData data;

        [SerializeField] protected int health;
        [SerializeField] protected EnemyPlace place;
        [SerializeField] protected float dashCooldown = 2f;
        [SerializeField] protected float dashSpeed = 5f;

        [SerializeField] protected float currentDashCooldown;
        [SerializeField] protected bool isPaused;
        [SerializeField] protected bool isInPlace;
        [SerializeField] protected bool isDashing;
        [SerializeField] protected bool isGameOver;
        [SerializeField] protected bool isDead;

        [SerializeField] protected Animator anim;
        // private EnemySpawnManager enemySpawnManager;
        public bool firstSpawned { get; private set; }


        public event Action<Enemy> OnDie;
        public event Action<Enemy> OnKilled;
        public event Action<Enemy> OnReachCaravan;
        public event Action OnDamaged;
 
        private void Update()
		{
            if (isPaused || isGameOver)
                return;
            if (isInPlace)
            {
                if (currentDashCooldown > 0)
                {
                    currentDashCooldown -= Time.deltaTime;
                }
                else {
                    Dash();
                }
            }
		}
		private void OnDestroy()
		{
            OnDie = null;
            OnKilled = null;
            OnReachCaravan = null;
            OnDamaged = null;
		}
		public virtual void Damaged()
        {
            if (isDead) return;
            OnDamaged?.Invoke();
            health--;
            if (health <= 0)
            {
                anim.SetTrigger("Dying");
                Die();
            }
            else {
                anim.SetTrigger("Damaged");
            }
        }
        public virtual void Ultimated()
        {
   
            isDead = true;
            StopAllCoroutines();
            place.Clear();
            OnKilled?.Invoke(this);
            isInPlace = false;
            anim.SetTrigger("Dying");
            LeanTween.cancel(gameObject);
            ParticleScript blood = ObjectPoolController.Instance.bloodPool.GetObject();
            blood.transform.position = transform.position;
            StartCoroutine(DieDelay());
        }
        protected virtual void Dash()
        {
            if (isPaused) return;
            if (isDead) return;
            if (!isDashing)
            {
                isDashing = true;
            }
            anim.SetBool("Run", true);
            transform.Translate(new Vector3(0, 0, -dashSpeed*Time.deltaTime), Space.World);
            if (transform.position.z < -15)
            {
                isDashing = false;
                ReachCaravan();
            }
            
        }
        public virtual void Die()
        {
            isDead = true;
            isDashing = false;
            isInPlace = false;
            StopAllCoroutines();
            place.Clear();
            OnDie?.Invoke(this);
            OnKilled?.Invoke(this);
            anim.SetTrigger("Dying");
            StartCoroutine(DieDelay());
            LeanTween.cancel(gameObject);
        }
        private IEnumerator DieDelay()
        {
            yield return new WaitForSeconds(4f);
            ObjectPoolController.Instance.goblinPool.Push(this);
        }
        public bool GetIsDead()
        {
            return isDead;
        }
        public virtual void ReachCaravan()
        {
            place.Clear();
            OnReachCaravan?.Invoke(this);
            ObjectPoolController.Instance.goblinPool.Push(this);
        }
        protected virtual void SetData()
        {
            health = data.health;
            dashCooldown = data.dashCooldown;
            dashSpeed = data.dashSpeed;
        }
        public virtual void Spawned(Vector3 pos)
        {
            if (!firstSpawned)
            {
                firstSpawned = true;
                GameStart();
            }
            anim.SetBool("Run", true);
            isDead = false;
            this.transform.position = pos;
            SetData();
            currentDashCooldown = dashCooldown;
            isInPlace = false;
            health = 1;
            Debug.Log("EnemySpawned");
            LeanTween.moveZ(gameObject, place.transform.position.z, 2).setOnComplete(SetInPlace);
        }
        protected virtual void SetInPlace()
        {
            if (!isDead)
            {
                isInPlace = true;
                anim.SetBool("Run", false);
            }
        }
        public virtual void SetPlace(EnemyPlace place)
        {
            this.place = place;
        }
        bool pausingLeanTween;
		public virtual void Pause()
		{
            anim.speed = 0;
            isPaused = true;
            LeanTween.pause(gameObject);
            pausingLeanTween = true;
        }
        public virtual void GameOver()
        {
            isGameOver = true;
        }
        public virtual void GameStart()
        {
            isGameOver = false;
        }
		public virtual void Continue()
		{
            isPaused = false;
            anim.speed = 1;
            if (pausingLeanTween)
            {
                pausingLeanTween = false;
                LeanTween.resume(gameObject);
            }
        }
	}
}
