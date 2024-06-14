using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CameraChangeController : MonoBehaviour
    {
        [SerializeField] private GameObject[] cameraList;

		private void Start()
		{
            ChangeToGameCamera();
		}
		public void ChangeToGameCamera()
        {
            cameraList[0].SetActive(true);
            cameraList[1].SetActive(false);
        }
        public void ChangeToUltimateCamera()
        {
            cameraList[0].SetActive(false);
            cameraList[1].SetActive(true);
        }
    }
}
