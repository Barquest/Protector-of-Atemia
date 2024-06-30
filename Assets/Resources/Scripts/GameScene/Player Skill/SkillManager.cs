using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class SkillManager : MonoBehaviour
    {
        [SerializeField] private Skill korvinSkill;
        [SerializeField] private Skill aronaSkill;
        [SerializeField] private PlayerController player;
        [SerializeField] private PlayerSwordController arona;
        [SerializeField] private PlayerShieldController korvin;

        [SerializeField] private EnemySpawnManager enemySpawnManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CameraChangeController cameraChangeController;
        [SerializeField] private GameUIController uiManager;



        public event Action<float> OnAronaAttackSuccess;
        public event Action OnAronaStartAttack;
        public event Action OnAronaBackToPosition;
        public event Action<float> OnKorvinDefendSuccess;

        public event Action OnSkillExecuted;

        private Skill currentSkillUsed;
        private bool currentSkillArona;
        private int currentLane;
		private void Start()
		{
            arona.OnAttackSuccess += AronaAttackSuccess;
            korvin.OnAttackSuccess += KorvinDefendSuccess;
            arona.OnStartAttacking += InvokeStartAttack;
            arona.OnBackToPosition += InvokeBackToPosition;
		}
        private void InvokeStartAttack()
        {
            OnAronaStartAttack?.Invoke();
        }
        private void InvokeBackToPosition()
        {
            OnAronaBackToPosition?.Invoke();
        }
        public void UseKorvinSkill()
        {
            korvinSkill.UseSkill();
            UseSkill(korvinSkill);
        }
        public void UseAronaSkill()
        {
            aronaSkill.UseSkill();
            UseSkill(aronaSkill);
        }
        private void UseSkill(Skill skill)
        {
            
            switch (skill.GetSkillType())
            {
                case SkillType.bladeDance:
                    OnSkillExecuted += BladeDance;
                    currentSkillArona = true;
                    PopupManager.Instance.Display(new PopupDebug("Arona Charging for 4 s...", 2f));
                    playerController.StopArona(4, SkillEffect);
                    break;
                case SkillType.coupdeThrust:
                    OnSkillExecuted += CoupDeThrust;
                    currentSkillArona = true;
                    currentLane = playerController.GetCurrentLane();
                    PopupManager.Instance.Display(new PopupDebug("Arona Charging for 2.5 s...", 2f));
                    playerController.StopArona(2.5f, SkillEffect);
                    break;
                case SkillType.kunaiRain:
                    currentSkillArona = false;
                    OnSkillExecuted += KunaiRain;
                    PopupManager.Instance.Display(new PopupDebug("Korvin use KUNAI RAIN!!", 2f));
                    SkillEffect();
                    break;
                case SkillType.shieldBarrier:
                    currentSkillArona = false;
                    OnSkillExecuted += ShieldBarrier;
                    PopupManager.Instance.Display(new PopupDebug("Korvin use SHIELD BARRIER!!", 2f));
                    SkillEffect();
                    break;
            }
        }
        public void BladeDance()
        {
            PopupManager.Instance.Display(new PopupDebug("Arona USE BLADE DANCE", 2f));
            enemySpawnManager.DamageAllEnemiesInScene();
        }
        public void ShieldBarrier()
        {
            korvin.ActivateBigShield();
            StartCoroutine(BigShieldDisactiveDelay(8f));
        }
        public void KunaiRain()
        {
            enemySpawnManager.DamageSeveralEnemies(3);
        }
        private IEnumerator BigShieldDisactiveDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            korvin.DisactivateBigShield();
        }
        public void CoupDeThrust()
        {
            Debug.Log("Coup De Thrust");
            PopupManager.Instance.Display(new PopupDebug("Arona using COUP DE THRUST", 2f));
            int leftLane = currentLane - 1;
            int rightLane = currentLane + 1;
            enemySpawnManager.DamageLaneEnemies(currentLane+2);
            if (leftLane > -3)
            {
                enemySpawnManager.DamageLaneEnemies(leftLane+2);
            }
            if (rightLane < 3)
            {
                enemySpawnManager.DamageLaneEnemies(rightLane+2);
            }
        }
        private void AronaAttackSuccess()
        {
            AddAronaSkillPoint(1);
            OnAronaAttackSuccess?.Invoke(GetAronaSkillPercentage());
            Debug.Log("Arona Attack Success");
        }
        private void KorvinDefendSuccess()
        {
            AddKorvinSkillPoint(1);
            OnKorvinDefendSuccess?.Invoke(GetKorvinSkillPercentage());
        }
        public void AddKorvinSkillPoint(int val)
        {
            korvinSkill.AddSkillPoint(val);
        }
        public void AddAronaSkillPoint(int val)
        {
            aronaSkill.AddSkillPoint(val);
        }
        public void AddBothSkillPoint(int val)
        {
            AddKorvinSkillPoint(val);
            AddAronaSkillPoint(val);
        }
        public float GetKorvinSkillPercentage()
        {
            return ((float)korvinSkill.GetCurrentSkillPoint() / (float)korvinSkill.GetCurrentMaxSkillPoint());
        }
        public float GetAronaSkillPercentage()
        {
            return ((float)aronaSkill.GetCurrentSkillPoint() / (float)aronaSkill.GetCurrentMaxSkillPoint());
        }
		private void OnDestroy()
		{
            OnAronaAttackSuccess = null;
            OnKorvinDefendSuccess = null;
            OnAronaAttackSuccess = null;
            OnAronaBackToPosition = null;
		}



        private void SkillEffect()
        {
            StartCoroutine(StartSkillEffect());
        }
        private IEnumerator StartSkillEffect()
        {
            Debug.Log("Ultimate Attack!!!");
            cameraChangeController.ChangeToUltimateCamera();
            enemySpawnManager.Pause();
            playerController.Pause();
            playerController.CenterPlayer();
            if (currentSkillArona)
            {
                playerController.AronaSkill();
            }
            else
            {
                playerController.KorvinSkill();
            }
            uiManager.Hide();
            yield return new WaitForSeconds(2f);
            OnSkillExecuted?.Invoke();
            cameraChangeController.ChangeToGameCamera();
            enemySpawnManager.Continue();
            playerController.Continue();
            uiManager.Show();
            OnSkillExecuted = null;
        }
    }
}
