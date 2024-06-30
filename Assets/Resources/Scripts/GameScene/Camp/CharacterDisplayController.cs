using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CharacterDisplayController : MonoBehaviour
    {
        [SerializeField] private Transform displayParent;

        public void EmptyDisplay()
        {
            foreach (Transform t in displayParent)
            {
                Destroy(t.gameObject);
            }
        }
        public void DisplayCharacter(GameObject prefab)
        {
            EmptyDisplay();
            Instantiate(prefab,displayParent.transform.position,displayParent.transform.rotation, displayParent);
        }
    }
}
