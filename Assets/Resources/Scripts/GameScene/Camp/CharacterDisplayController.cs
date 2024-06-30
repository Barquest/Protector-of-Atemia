using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CharacterDisplayController : MonoBehaviour
    {
        [SerializeField] private Transform displayParent;
        [SerializeField] private CharacterDisplay display;

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
            GameObject p = Instantiate(prefab,displayParent.transform.position,displayParent.transform.rotation, displayParent);
            display = p.GetComponent<CharacterDisplay>();
        }
        public void EquipWeapon(Accessories data)
        {
            display.ChangeWeapon(data);
        }
    }
}
