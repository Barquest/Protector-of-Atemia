using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MadGeekStudio.ProtectorOfAtemia.Systems.Audio;

using Random = UnityEngine.Random;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
	public class EnemySpawnManager : Subject, IPausable
	{
		public LevelSelectData DebugLevelSelected;
		[SerializeField] private int currentWaveIndex;
		[SerializeField] private LevelSelectData currentLevel;
		[SerializeField] private WaveData currentWave;
		[SerializeField] private List<WaveData> waves;
		[SerializeField] private List<Enemy> enemyInScene;
		[SerializeField] private List<Enemy> enemyReachedCaravanList;
		[SerializeField] private List<EnemyPlace> enemyPlaces;
		[SerializeField] private bool isPaused;
		[SerializeField] private float delay;

		public event Action OnWaveCleared;

		private void Start()
		{
			//InvokeRepeating("SpawnEnemiesRandom", 1, 4);
			AddObserver(GameManager.Instance);

			
		}
		public void StartSpawnManager()
		{
			if (!GameManager.Instance.isDebug)
			{
				SetWaves();
				currentWaveIndex = 0;
				Invoke("SpawnEnemyWave", 1);
			}
			else
			{
				SetDebugWaves();
				currentWaveIndex = 0;
				Invoke("SpawnEnemyWave", 1);
			}
		}
		private void Update()
		{
			if (isPaused) return;
			if (delay > 0)
			{
				delay -= Time.deltaTime;
			}
			else if (delay < 0)
			{
				delay = 0;
				SpawnEnemyWave();
			}
		}
		private void OnDestroy()
		{
			OnWaveCleared = null;
		}
		private void SetWaves()
		{
			currentLevel = GlobalGameManager.Instance.GetLevelData();

			for (int i = 0; i < currentLevel.stageData.waveList.Count; i++)
			{
				waves.Add(currentLevel.stageData.waveList[i]);
			}
		}
		private void SetDebugWaves()
		{
			currentLevel = DebugLevelSelected;
			GlobalGameManager.Instance.SetLevelData(currentLevel);
			for (int i = 0; i < currentLevel.stageData.waveList.Count; i++)
			{
				waves.Add(currentLevel.stageData.waveList[i]);
			}
		}
		public void GameOver()
		{
			for (int i = 0; i < enemyInScene.Count; i++)
			{
				enemyInScene[i].GameOver();
			}
		}
		public void GameStart()
		{
			for (int i = 0; i < enemyInScene.Count; i++)
			{
				enemyInScene[i].GameStart();
			}
		}
		public void DamageAllEnemiesInScene()
		{
			StartCoroutine(StartDamageAllEnemies());
		}
		private IEnumerator StartDamageAllEnemies()
		{
			for (int i = 0; i < enemyInScene.Count; i++)
			{
				ParticleScript particle = ObjectPoolController.Instance.particlePool.GetObject();
				particle.transform.position = enemyInScene[i].transform.position;
				enemyInScene[i].Ultimated();
				AudioManager.Instance.PlaySfx("Sword Hit");
				yield return new WaitForSeconds(0.1f);
			}
			enemyInScene.Clear();
			yield return new WaitForSeconds(1);
			CheckNoEnemyInScene();
		}
		public void DamageLaneEnemies(int whichLane)
		{
			Debug.Log("DamageLaneEnemies " + whichLane);
			StartCoroutine(StartDamageLaneEnemies(whichLane));
		}
		private IEnumerator StartDamageLaneEnemies(int whichLane)
		{
			Debug.Log("StartDamageLaneEnemies");
			yield return new WaitForSeconds(0.1f);

			List<Enemy> targets = new List<Enemy>(enemyInScene);
			for (int i = 0; i < targets.Count; i++)
			{
				if (targets[i].GetX() == whichLane)
				{
					ParticleScript particle = ObjectPoolController.Instance.particlePool.GetObject();
					particle.transform.position = targets[i].transform.position;
					targets[i].Damaged();
					AudioManager.Instance.PlaySfx("Sword Hit");
					yield return new WaitForSeconds(0.1f);
				}
			}
			yield return new WaitForSeconds(1);
			CheckNoEnemyInScene();
		}
		public void DamageSeveralEnemies(int count)
		{
			Debug.Log("DamageSeveralEnemies " + count);
			StartCoroutine(StartDamageSeveralEnemies(count));
		}
		private IEnumerator StartDamageSeveralEnemies(int countToKill)
		{
			yield return new WaitForSeconds(0.5f);
			int currentKilled = 0;
			Debug.Log("StartDamageSeveralEnemies");
			List<Enemy> targets = new List<Enemy>(enemyInScene);
			for (int i = 0; i < targets.Count; i++)
			{
				if (currentKilled < countToKill)
				{
					ParticleScript particle = ObjectPoolController.Instance.particlePool.GetObject();
					particle.transform.position = targets[i].transform.position;
					targets[i].Damaged();
					AudioManager.Instance.PlaySfx("Sword Hit");
					currentKilled++;
					yield return new WaitForSeconds(0.1f);
				}
			}
			yield return new WaitForSeconds(1);
			CheckNoEnemyInScene();
		}
		public void Continue()
		{
			if (isPaused)
			{
				isPaused = false;
				for (int i = 0; i < enemyInScene.Count; i++)
				{
					enemyInScene[i].Continue();
				}
			}
		}
		public void Pause()
		{
			isPaused = true;
			for (int i = 0; i < enemyInScene.Count; i++)
			{
				enemyInScene[i].Pause();
			}
		}
		public void RemoveEnemyInScene(Enemy enemy)
		{
			enemyInScene.Remove(enemy);
			CheckNoEnemyInScene();
		}
		public void KilledByPlayer(Enemy enemy)
		{
			NotifyObservers(Act.Add1KillCount);
		}
		public void CheckNoEnemyInScene()
		{
			if (enemyInScene.Count == 0)
			{
				SpawnEnemyWave();
			}
		}
		private IEnumerator StartSpawnEnemyWave()
		{
			yield return new WaitForSeconds(0.1f);
			if (currentWaveIndex < waves.Count)
			{
				currentWave = waves[currentWaveIndex];
				for (int i = 0; i < currentWave.units.Count; i++)
				{
					SpawnEnemy(currentWave.units[i].type,currentWave.units[i].x, currentWave.units[i].z);
				}
				if (waves[currentWaveIndex].ForceNextWaveDelay > 0)
				{
					delay = waves[currentWaveIndex].ForceNextWaveDelay;
				}
				currentWaveIndex++;
			}
			else
			{
				Debug.Log("All Wave Cleared");
				OnWaveCleared?.Invoke();
			}
		}
		public void SpawnEnemyWave()
		{

			StartCoroutine(StartSpawnEnemyWave());
		}
		public void SpawnEnemiesRandom()
		{
			int r = Random.Range(1, 5);
			for (int i = 0; i < r; i++)
			{
				SpawnEnemyRandom(UnitType.Goblin);
			}
		}
		public void SpawnEnemyRandom(UnitType type)
		{
			SpawnEnemy(type,Random.Range(0,5), Random.Range(0,5));
		}
		public Enemy GetEnemy(UnitType type)
		{
			switch (type)
			{
				case UnitType.Goblin:
					return ObjectPoolController.Instance.goblinPool.GetObject();
				case UnitType.GoblinBamboo:
					return ObjectPoolController.Instance.goblinBambooPool.GetObject();
				case UnitType.GoblinCrossbow:
					return ObjectPoolController.Instance.goblinCrossbowPool.GetObject();
				case UnitType.GoblinKujang:
					return ObjectPoolController.Instance.goblinKujangPool.GetObject();
				case UnitType.GoblinSumpit:
					return ObjectPoolController.Instance.goblinSumpitPool.GetObject();
			}
			return null;
		}
		public void SpawnEnemy(UnitType type,int x, int z)
		{
			Debug.Log("Try SpawnEnemy");
			Enemy enemy = GetEnemy(type);
			for (int i = 0; i < enemyPlaces.Count; i++)
			{
				if (enemyPlaces[i].GetX() == x && enemyPlaces[i].GetZ() == z)
				{
					if (!enemyPlaces[i].CheckIsOccuipied())
					{
						enemyPlaces[i].Occuppy();
						Debug.Log("SpawnEnemy");
						Vector3 position = new Vector3(enemyPlaces[i].transform.position.x, enemyPlaces[i].transform.position.y, enemyPlaces[i].transform.position.z+50);
						enemy.SetPlace(enemyPlaces[i]);
						enemy.transform.position = position;
						enemy.Continue();
						if (!enemy.firstSpawned)
						{
							enemy.OnDie += RemoveEnemyInScene;
							enemy.OnReachCaravan += RemoveEnemyInScene;
							enemy.OnReachCaravan += EnemyReachedCaravan;
							enemy.OnKilled += KilledByPlayer;
						}
						enemy.Spawned(position);
						enemyInScene.Add(enemy);
						break;
					}
					else {
						break;
					}
				}
			}
		}
		public void EnemyReachedCaravan(Enemy enemy)
		{
			enemyReachedCaravanList.Add(enemy);
			NotifyObservers(Act.DamageCaravan);
		}
	}
}
