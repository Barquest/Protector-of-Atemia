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

        public void Setup(CharacterData data)
        {
            this.data = data;
        }
    }
}
