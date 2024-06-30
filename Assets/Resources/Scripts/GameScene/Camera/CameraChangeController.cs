using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CameraChangeController : MonoBehaviour
    {
        [SerializeField] private GameObject[] cameraList;
        [SerializeField] private int index;
		private void Start()
		{
            ChangeToGameCamera();
		}
        public void ChangeCameraToIndex(int index)
        {
            cameraList[this.index].SetActive(false);
            cameraList[index].SetActive(true);
            this.index = index;
        }
        
		public void ChangeToGameCamera()
        {
            cameraList[0].SetActive(true);
            index = 0;
            cameraList[1].SetActive(false);
        }
        public void ChangeToUltimateCamera()
        {
            cameraList[0].SetActive(false);
            index = 1;
            cameraList[1].SetActive(true);
        }
    }
}
