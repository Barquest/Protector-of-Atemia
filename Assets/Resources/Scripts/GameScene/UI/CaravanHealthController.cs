using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CaravanHealthController : MonoBehaviour
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private GameObject healthIcon;
        [SerializeField] private Transform parentTransform;

        public void DisplayCurrentHealth(int value)
        {
            currentHealth = value;
            foreach (Transform t in parentTransform)
            {
                Destroy(t.gameObject);
            }
            for (int i = 0; i < currentHealth; i++)
            {
                Instantiate(healthIcon, parentTransform);
            }
        }
    }
}
