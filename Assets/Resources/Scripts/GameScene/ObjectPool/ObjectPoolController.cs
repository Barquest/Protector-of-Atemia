using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ObjectPoolController : MonoBehaviour
    {
        public static ObjectPoolController Instance;

        public GoblinPool goblinPool;
        public GoblinSumpitPool goblinSumpitPool;
        public GoblinKujangPool goblinKujangPool;
        public GoblinCrossbowPool goblinCrossbowPool;
        public GoblinBambooPool goblinBambooPool;
        public ParticlePool particlePool;
        public ParticlePool redImpactPool;
        public ParticlePool bloodPool;
        public ParticlePool slashBlueMediumPool;
        public BulletPool sumpitBulletPool;
        public BulletPool arrowBulletPool;
        void Awake()
        {
            Instance = this;
        }

    }
    public class DataStore<T>
    { 
        public T Data { get; set; }
    }
}