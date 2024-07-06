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
        [SerializeField] private AmbushManager ambushManager;

        [SerializeField] private GameObject ambushDisplay;

        private Rigidbody rb;
        private Vector3 movement;
        private bool isWalking;
        private bool cantWalk;
        private bool isTriggerActive;
        private LevelSelectButton currentLevelDisplay;

        void Start()
        {
            Vector3 pos = GlobalGameManager.Instance.GetPlayerData().playerPositionInWorld;
            if (pos != Vector3.zero)
            {
                transform.position = pos;
            }
            
            ambushManager.OnAmbush += FreezeMovement;
            ambushManager.OnAmbush += DisplayAmbush;
            rb = GetComponent<Rigidbody>();
            ambushDisplay.SetActive(false);
            StartCoroutine(TriggerActiveDelay());
        }
        private IEnumerator TriggerActiveDelay()
        {
            yield return new WaitForSeconds(1);
            isTriggerActive = true;
        }
        void Update()
        {
            // Get input from the floating joystick
            if (cantWalk)
                return;
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
                ambushManager.ProgressAmbush();
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
        private void DisplayAmbush()
        {
            ambushDisplay.SetActive(true);
        }
        private void FreezeMovement()
        {
            cantWalk = true;
            movement.z = 0;
            movement.x = 0;
            isWalking = false;
            aronaAnim.SetBool("Walk", false);
            torvinAnim.SetBool("Walk", false);
        }
        private void UnFreezeMovement()
        {
            cantWalk = false;
        }

        void FixedUpdate()
        {
            // Move the player
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
		private void OnTriggerEnter(Collider other)
		{
            if (!isTriggerActive) return;
            if (other.CompareTag("Level Select"))
            {
                currentLevelDisplay = other.GetComponent<LevelSelectButton>();
                levelSelectManager.DisplayLevel(currentLevelDisplay);
            }
		}
		private void OnTriggerExit(Collider other)
		{
            if (!isTriggerActive) return;
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
