using System.Collections;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class GoblinCrossbow : Enemy
    {
        [SerializeField] private bool isAlreadyAttack;
        [SerializeField] private Transform goblinTransform;
        [SerializeField] private Transform tempatPeluru;
        private float afterAttackCd = 5;

        private IEnumerator attackCoroutine;
        public override void Spawned(Vector3 pos)
        {
            base.Spawned(pos);
            isAlreadyAttack = false;
        }
        protected override void AttackDash()
        {
            if (isInPlace)
            {
                if (currentDashCooldown > 0)
                {
                    currentDashCooldown -= Time.deltaTime;
                }
                else if (currentDashCooldown < 0)
                {

                    Attack();
                    currentDashCooldown = afterAttackCd;
                    attackCoroutine = AttackDelay();
                    StartCoroutine(attackCoroutine);
                  
                }
            }
        }

        public override void Damaged()
        {
            base.Damaged();
            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);
        }
        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(1.5f);
            Debug.Log("Attacking");
            Bullet bullet = ObjectPoolController.Instance.arrowBulletPool.GetObject();
            bullet.transform.rotation = goblinTransform.transform.rotation;
            bullet.transform.position = tempatPeluru.transform.position;
        }
        protected virtual void Attack()
        {
            anim.SetTrigger("Attack");
        }
    }
}
