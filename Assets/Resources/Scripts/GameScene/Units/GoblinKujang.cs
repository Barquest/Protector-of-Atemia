using System.Collections;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class GoblinKujang : Enemy
    {
        [SerializeField] private bool isAlreadyAttack;
        [SerializeField] private Transform goblinTransform;
        [SerializeField] private float attackRadius = 3f;
        private float afterAttackCd = 2;

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
                    if (!isAlreadyAttack)
                    {
                        Attack();
                        currentDashCooldown = afterAttackCd;
                        attackCoroutine = AttackDelay();
                        StartCoroutine(attackCoroutine);
                    }
                }
            }
        }
        public override void Damaged()
        {
            base.Damaged();
            StopCoroutine(attackCoroutine);
        }
        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(0.4f);
            ParticleScript particle = ObjectPoolController.Instance.particlePool.GetObject();
            particle.transform.position = transform.position;
            particle.transform.rotation = goblinTransform.transform.rotation;
            RaycastHit hit;
            Debug.Log("Attacking");

            if (Physics.Raycast(transform.position, goblinTransform.forward, out hit, attackRadius, playerMask))
            {
                Debug.Log("Raycast hit " + hit.transform.gameObject.name);
                if (hit.transform.CompareTag("Sword"))
                {
                    hit.transform.GetComponent<PlayerSwordController>().AttackCancel();
                }
            }
        }
        protected virtual void Attack()
        {
            anim.SetTrigger("Attack");
        }
    }
}
