using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class CharacterDisplay : MonoBehaviour
    {
        [SerializeField] private CharacterData data;
        [SerializeField] private Accessories accessories;
        [SerializeField] private Equipment equipment;

        [SerializeField] private GameObject defaultWeaponPrefab;
        [SerializeField] private Transform weaponParent;
        public void Setup(CharacterData data)
        {
            this.data = data;
        }
        public void ChangeWeapon(Accessories acc)
        {
            if (acc == null)
            {
                Debug.Log("ChangeWeapon Null");
                EmptyWeapon();
                Instantiate(defaultWeaponPrefab, weaponParent.transform.position, weaponParent.rotation,weaponParent);
            }
            else {
                Debug.Log("ChangeWeapon Not Null");
                EmptyWeapon();
                GameObject prefab = ItemDatabase.Instance.GetAccessoriesData(acc.id).displayPrefab;
                Instantiate(prefab, weaponParent.transform.position, weaponParent.rotation, weaponParent);
            }
        }
        private void EmptyWeapon()
        {
            foreach (Transform t in weaponParent)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
