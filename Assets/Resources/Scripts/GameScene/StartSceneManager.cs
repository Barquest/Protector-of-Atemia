using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class StartSceneManager : MonoBehaviour
    {
        [SerializeField] private LoadingManager loadingManager;
        public void GoToMainMenu()
        {
            loadingManager.LoadScene(1);
        }
    }
}
