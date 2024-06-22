using System;
using UnityEngine;
using System.Collections;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;
using System.Collections.Generic;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public class GameManager : MonoBehaviour, IObserver
	{
		public static GameManager Instance;

		public StageGameData StageGameData;
		
		[SerializeField] private PlayerSwordController playerSwordController;
		[SerializeField] private PlayerController playerController;
		[SerializeField] private EnemySpawnManager enemySpawnManager;
		[SerializeField] private CameraShake cameraShake;
		[SerializeField] private GameUIController uiManager;
		[SerializeField] private UltimateUIController ultimateUIController;
		[SerializeField] private GameOverLoseUIController gameOverLoseUIController;
		[SerializeField] private GameOverWinUIController gameOverWinUIController;
		[SerializeField] private PauseMenuUIController pauseMenuUIController;
		[SerializeField] private CameraChangeController cameraChangeController;
		[SerializeField] private LoadingManager loadingManager;
		[SerializeField] private LaneHelperController laneHelperController;
		[SerializeField] private DialogueManager dialogueManager;

		[SerializeField] private GameData gameData;
		[SerializeField] private int caravanHealth;
		[SerializeField] private bool isPaused;
		[SerializeField] private bool gameIsOver;
		[SerializeField] private float ultimatePointOnTriggered;
		[SerializeField] private float ultimatePoint;
		[SerializeField] private float ultimatePointMax;

		public bool isDebug;

		private readonly float cameraShakeDuration = 0.1f;
		private readonly float cameraShakePower = 0.25f;

		public event Action<int> OnCaravanHealthChanged;

		int starterHealth;

		private void Awake()
		{
			Instance = this;
			LeanTween.reset();
		}
		private void Start()
		{
			LeanTween.reset();
			SetData();
			gameIsOver = false;
			enemySpawnManager.OnWaveCleared += GameOverWin;
			ultimateUIController.OnSkillButtonPressed += UltimateAttack;
			StageGameData.AddKillCount(0);
			OnCaravanHealthChanged?.Invoke(caravanHealth);
			starterHealth = caravanHealth;
			playerController.SetShieldAction(AddUltimatePoint);
			playerController.SetSwordAction(AddUltimatePoint);
			playerController.SetCanMove(false);
			playerSwordController.OnStartAttacking += ultimateUIController.DisactivateSkillButton;
			playerSwordController.OnBackToPosition += ultimateUIController.ActivateSkillButton;
			dialogueManager.AfterDialogueComplete += AfterDialogueComplete;
			AudioManager.Instance.StopMusic();
			//laneHelperController.StartDecaying();
			dialogueManager.Hide();
			StartCoroutine(StartDelay());
			//enemySpawnManager.GameStart();
		}
		private IEnumerator StartDelay()
		{
			uiManager.Hide();
			yield return new WaitForSeconds(0.5f);
			dialogueManager.ShowDialogueGroup(0);
		}
		public void ShowGameUI()
		{
			uiManager.Show();
		}
		public void HideGameUI()
		{
			uiManager.Hide();
		}
		public void AfterDialogueComplete()
		{
			enemySpawnManager.StartSpawnManager();
			AudioManager.Instance.PlayMusic("Battle Music");
			playerController.SetCanMove(true);
			laneHelperController.StartDecaying();
		}
		public void PauseGame()
		{
			playerController.SetCanMove(false);
			enemySpawnManager.Pause();
			pauseMenuUIController.Show();
			uiManager.Hide();
			//LeanTween.pauseAll();
		}
		public void ContinueGame()
		{
			enemySpawnManager.Continue();
			playerController.SetCanMove(true);
			pauseMenuUIController.Hide();
			uiManager.Show();
			//LeanTween.resumeAll();

		}
		
		public void AddKillCount(int value)
		{
			StageGameData.AddKillCount(value);
		}
		public void AddCaravanDamagedCount(int value)
		{
			StageGameData.AddDamagedCount(value);
		}
		public void AddUltimatePoint()
		{
			if (ultimatePoint < ultimatePointMax)
			{
				ultimatePoint += ultimatePointOnTriggered;
				if (ultimatePoint > ultimatePointMax)
					ultimatePoint = ultimatePointMax;
				ultimateUIController.UpdateSliderValue(ultimatePoint, ultimatePointMax);
			}
		}
		private void UltimateAttack()
		{
			StartCoroutine(StartUltimateAttack());
		}
		private IEnumerator StartUltimateAttack()
		{
			Debug.Log("Ultimate Attack!!!");
			cameraChangeController.ChangeToUltimateCamera();
			ultimatePoint = 0;
			enemySpawnManager.Pause();
			playerController.Pause();
			playerController.CenterPlayer();
			playerController.Skill();
			uiManager.Hide();
			ultimateUIController.UpdateSliderValue(ultimatePoint, ultimatePointMax);
			yield return new WaitForSeconds(2f);
			enemySpawnManager.DamageAllEnemiesInScene();
			cameraChangeController.ChangeToGameCamera();
			enemySpawnManager.Continue();
			playerController.Continue();
			uiManager.Show();
		}
		private void SetData()
		{
			caravanHealth = gameData.caravanHealth;
		}
		public void DamageCaravan()
		{
			CaravanDamaged();
		}
		private void CaravanDamaged()
		{
			if (caravanHealth > 0)
			{
				caravanHealth--;
				OnCaravanHealthChanged?.Invoke(caravanHealth);
				if (caravanHealth <= 0)
				{
					GameOverLose();
				}
			}
			else
			{
				GameOverLose();
			}
		}
		private void GameOverLose()
		{
			if (!gameIsOver)
			{
				gameIsOver = true;
				Debug.Log("Game Over Lose");
				playerController.SetCanMove(false);
				gameOverWinUIController.ShowLose();
				enemySpawnManager.GameOver();
				PlayerRewarding();

			}
		}
		private void GameOverWin()
		{
			if (!gameIsOver)
			{
				gameIsOver = true;
				Debug.Log("Game Over Win");
				playerController.SetCanMove(false);
				GlobalGameManager.Instance.UnlockNewLevel();
				GlobalGameManager.Instance.SaveGame();
				uiManager.Hide();
				enemySpawnManager.GameOver();
				PlayerRewarding();
				gameOverWinUIController.ShowWin(TotalGoldReward());
			}
		}
		private int TotalGoldReward()
		{

			int goldPool = GlobalGameManager.Instance.GetLevelData().goldPool;
			float rumus = (float)caravanHealth / (float)starterHealth;
			return Mathf.RoundToInt(rumus * (float)goldPool);
		}
		public void PlayerRewarding()
		{
			
			Debug.Log("Rumus : " +caravanHealth + " / " + starterHealth + " Total Gold Reward : " + TotalGoldReward());
			GlobalGameManager.Instance.GetPlayerData().money += TotalGoldReward();
		}
		public void GotoMainMenu()
		{
			loadingManager.LoadScene(1);
		}
		public void Replay()
		{
			loadingManager.LoadScene(3);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				if (isPaused)
				{
					Continue();
				}
				else
				{
					Pause();
				}
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				ultimatePoint = ultimatePointMax;
				ultimateUIController.UpdateSliderValue(ultimatePoint,ultimatePointMax);
			}
		}
		private void Pause()
		{
			isPaused = true;
			playerSwordController.Pause();
			playerController.Pause();
			enemySpawnManager.Pause();
		}
		private void Continue()
		{
			isPaused = false;
			playerSwordController.Continue();
			playerController.Continue();
			enemySpawnManager.Continue();
		}

		public void CameraShake()
		{
			cameraShake.Shake(cameraShakeDuration, cameraShakePower);
		}
		public void CameraShake(float dur,float power)
		{
			cameraShake.Shake(cameraShakeDuration, cameraShakePower);
		}

		public void OnNotify(Act act)
		{
			Debug.Log("OnNotify");
			if (act == Act.Attack)
			{
				CameraShake();
			}
			else if (act == Act.DamageCaravan)
			{
				CaravanDamaged();
			}
			else if (act == Act.Add1KillCount)
			{
				AddKillCount(1);
			}
		}
	}
}
