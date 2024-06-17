using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class TopDownJoystickMovement : MonoBehaviour
    {
        
        
        public float moveSpeed = 5f;
        public FloatingJoystick floatingJoystick; // Reference to the Floating Joystick
        [SerializeField] private Animator aronaAnim;
        [SerializeField] private Animator torvinAnim;
        [SerializeField] private LevelSelectManager levelSelectManager;

        private Rigidbody rb;
        private Vector3 movement;
        private bool isWalking;
        private LevelSelectButton currentLevelDisplay;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            // Get input from the floating joystick
            movement.x = floatingJoystick.Horizontal;
            movement.z = floatingJoystick.Vertical;

            if (movement != Vector3.zero)
            {
                float angle = Mathf.Atan2(-movement.z, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                if (!isWalking)
                {
                    aronaAnim.SetBool("Walk", true);
                    torvinAnim.SetBool("Walk", true);
                    isWalking = true;
                }
            }
            else {
                if (isWalking)
                {
                    aronaAnim.SetBool("Walk", false);
                    torvinAnim.SetBool("Walk", false);
                    isWalking = false;
                }
            }
        }

        void FixedUpdate()
        {
            // Move the player
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
		private void OnTriggerEnter(Collider other)
		{
            if (other.CompareTag("Level Select"))
            {
                currentLevelDisplay = other.GetComponent<LevelSelectButton>();
                levelSelectManager.DisplayLevel(currentLevelDisplay);
            }
		}
		private void OnTriggerExit(Collider other)
		{
            if (other.CompareTag("Level Select"))
            {
                if (currentLevelDisplay == other.GetComponent<LevelSelectButton>())
                {
                    levelSelectManager.Hide();
                }
            }
        }
	}
}
