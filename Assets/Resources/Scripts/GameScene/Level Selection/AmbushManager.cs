using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class AmbushManager : MonoBehaviour
    {
        [SerializeField] private float maxAmbushDelay;
        [SerializeField] private float minAmbushDelay;
        [SerializeField] private float currentAmbushDelay;
        [SerializeField] private bool cantBeAmbushed;
        [SerializeField] private Transform playerTransform;

        [SerializeField] private LevelSelectManager levelSelect;
        [SerializeField] private List<LevelSelectData> ambushLevel = new List<LevelSelectData>();
        

        public event Action OnAmbush;
		private void Start()
		{
            if (GlobalGameManager.Instance.isDebug)
            {
                cantBeAmbushed = true;
            }
            else {
                cantBeAmbushed = false;
                BeginAmbush();
            }
		}
		public void BeginAmbush()
        {
            currentAmbushDelay = UnityEngine.Random.Range(minAmbushDelay, maxAmbushDelay);
        }
        public void ProgressAmbush()
        {
            if (cantBeAmbushed) return;

            if (currentAmbushDelay > 0)
            {
                currentAmbushDelay -= Time.deltaTime;
            } else if (currentAmbushDelay < 0)
            {
                
                Ambush();
                currentAmbushDelay = 0;
            }
        }
        public void Ambush()
        {
            OnAmbush?.Invoke();
            StartCoroutine(AmbushDelay());
        }
        private IEnumerator AmbushDelay()
        {
            PopupManager.Instance.Display(new PopupDebug("Ambushed!!",2f));
            yield return new WaitForSeconds(2f);
            LevelSelectData data = ambushLevel[UnityEngine.Random.Range(0, ambushLevel.Count)];
            GlobalGameManager.Instance.GetPlayerData().playerPositionInWorld = playerTransform.transform.position;
            GlobalGameManager.Instance.SetLevelData(data);
            MainMenuManager.Instance.LoadLevel(3);

        }
		private void OnDestroy()
		{
            OnAmbush = null;
		}


	}
}
