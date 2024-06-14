using System;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class LevelSelectCharacter : MonoBehaviour
    {
        [SerializeField] private Queue<Vector3> targets = new Queue<Vector3>();
        [SerializeField] private Animator swordAnim;
        [SerializeField] private Animator shieldAnim;
        [SerializeField] private Transform sword;
        [SerializeField] private Transform shield;

        [SerializeField] private Vector3 levelTarget;

        public event Action OnWalkOver;
        
        public void WalkTo(Vector3 targetPos)
        {
            LeanTween.move(this.gameObject, targetPos, 1f).setOnComplete(WalkOver);
            levelTarget = targetPos;
        }
        public void SetAnimWalk(bool data)
        {
            swordAnim.SetBool("Walk",data);
            shieldAnim.SetBool("Walk",data);
        }
        public void Walks(List<Vector3> targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                this.targets.Enqueue(targets[i]);
            }
            Vector3 target = this.targets.Dequeue();
            levelTarget = target;
            LeanTween.move(this.gameObject, target, 1f).setOnComplete(WalkOver);
        }
        void Update()
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = levelTarget - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = 30f * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
           // sword.rotation = Quaternion.LookRotation(newDirection);
            //shield.rotation = Quaternion.LookRotation(newDirection);
        }
        private void WalkOver()
        {
            if (targets.Count > 0)
            {
                Debug.Log("Walk Over Still have Target");
                Vector3 target = targets.Dequeue();
                levelTarget = target;
                LeanTween.move(this.gameObject, target, 1f).setOnComplete(WalkOver);
            }
            else {
                Debug.Log("Walk Over dont have Target");
                OnWalkOver?.Invoke();
            }
        }
		private void OnDestroy()
		{
            OnWalkOver = null;
		}
	}
}
