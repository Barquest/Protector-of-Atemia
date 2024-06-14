using System;
using UnityEngine;
using DentedPixel.LTExamples;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public class PlayerController : MonoBehaviour,IPausable
	{

		// Start is called before the first frame update
		[SerializeField] private bool movingHorizontal;
		[SerializeField] private bool cantMove;
		[SerializeField] private int currentLane;
		[SerializeField] private int maxLane = 2;
		[SerializeField] private float laneToLaneInterval = 0.25f;
		[SerializeField] private PlayerSwordController playerSwordController;
		[SerializeField] private PlayerShieldController playerShieldController;

		private bool isPaused;
		private void Start()
		{
			//playerSwordController.OnBackToPosition += ResetSwordPosition;
		}
		private void OnDestroy()
		{
			//playerSwordController.OnBackToPosition -= ResetSwordPosition;
		}
		private void Update()
		{
			if (Input.GetKey(KeyCode.A))
			{
				MoveLeft();
			}
			if (Input.GetKey(KeyCode.D))
			{
				MoveRight();
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				SwordAttack();
			}
		}
		public void SetSwordAction(Action act)
		{
			playerSwordController.OnAttackSuccess += act;
		}
		public void SetShieldAction(Action act)
		{
			playerShieldController.OnAttackSuccess += act;
		}
		public void CenterPlayer()
		{
			currentLane = 0;
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
			DoneMove();
		}
		public void SetCanMove(bool value)
		{
			cantMove = !value;
		}
		public void SwordAttack()
		{
			if (isPaused || cantMove) return;
			playerSwordController.Attack(currentLane*2);
		}
		public void Skill()
		{
			playerSwordController.Skill();
		}
		public void MoveLeft()
		{
			if (isPaused || cantMove) return;
			if (!movingHorizontal && currentLane > -maxLane)
			{
				movingHorizontal = true;
				LeanTween.moveX(gameObject, 2 * (currentLane - 1), laneToLaneInterval).setOnComplete(DoneMove);
				currentLane--;
				playerSwordController.DashLeft();
				playerShieldController.DashLeft();
			}
		}
		public void MoveRight()
		{
			if (isPaused || cantMove) return;
			if (!movingHorizontal && currentLane < maxLane)
			{
				movingHorizontal = true;
				LeanTween.moveX(gameObject, 2 * (currentLane + 1), laneToLaneInterval).setOnComplete(DoneMove);
				currentLane++;
				playerSwordController.DashRight();
				playerShieldController.DashRight();
			}
		}
		private void DoneMove()
		{
			movingHorizontal = false;
		}

		public void Pause()
		{
			isPaused = true;
			LeanTween.pauseAll();
		}

		public void Continue()
		{
			isPaused = false;
			LeanTween.resumeAll();
		}
	}
}