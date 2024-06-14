using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ObjectPoolController : MonoBehaviour
    {
        public static ObjectPoolController Instance;

        public GoblinPool goblinPool;
        public ParticlePool particlePool;
        public ParticlePool redImpactPool;
        void Awake()
        {
            Instance = this;
        }

    }
}