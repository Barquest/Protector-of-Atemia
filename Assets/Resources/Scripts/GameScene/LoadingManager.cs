using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Image loadingBarFill;

        LoadingScreenTips loadingScreenTips;
		private void Start()
		{
            loadingScreenTips = GetComponent<LoadingScreenTips>();
		}
		public void LoadScene(int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }
        public void LoadScene(string sceneName)
        {
            loadingScreenTips.DisplayRandomTips();
            int sceneId = SceneManager.GetSceneByName(sceneName).buildIndex;
            if (sceneId >= 0)
            {
                StartCoroutine(LoadSceneAsync(sceneId));
            }
            else
            {
                Debug.Log("Scene id : " + sceneId);
            }                
        }


        IEnumerator LoadSceneAsync(int sceneId)
        {
            loadingScreenTips.DisplayRandomTips();
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
            loadingScreen.SetActive(true);
            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
                loadingBarFill.fillAmount = progressValue;

                yield return null;
            }
        }
    }
}
