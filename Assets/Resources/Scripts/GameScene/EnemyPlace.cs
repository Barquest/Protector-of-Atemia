using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class EnemyPlace : MonoBehaviour
    {
        [SerializeField] private int x;
        [SerializeField] private int z;
        [SerializeField] private bool isOccupied;

        public int GetX()
        {
            return x;
        }
        public int GetZ()
        {
            return z;
        }
        public void Occuppy()
        {
            isOccupied = true;
        }
        public bool CheckIsOccuipied()
        {
            return isOccupied;
        }
        public void Clear()
        {
            isOccupied = false;
        }
    }
}
