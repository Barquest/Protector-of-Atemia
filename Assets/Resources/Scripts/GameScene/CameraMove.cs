using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Vector3 Origin;
        [SerializeField] private Vector3 Difference;
        [SerializeField] private Vector3 ResetCamera;
        [SerializeField] private Camera mainCam;

        [SerializeField] private bool drag = false;



        private void Start()
        {
            mainCam = Camera.main;
            ResetCamera = mainCam.transform.position;
        }


        private void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,mainCam.transform.position.z));

                Difference = pos - mainCam.transform.position;
               
                if (drag == false)
                {
                    drag = true;
                    Origin = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.z));
                }
            }
            else
            {
                drag = false;
            }

            if (drag)
            {
                Vector3 target = Origin - Difference;
                target = new Vector3(target.x, mainCam.transform.position.y, target.z);
                mainCam.transform.position = target;
            }

            if (Input.GetMouseButton(1))
                mainCam.transform.position = ResetCamera;

        }
    }
}
