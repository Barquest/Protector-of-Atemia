using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class ParticlePool : MonoBehaviour
    {
        [SerializeField] protected int amountToPool = 5;
        [SerializeField] protected ParticleScript prefab;
        protected Stack<ParticleScript> pooledObject = new Stack<ParticleScript>();

        private void Start()
        {
            pooledObject = new Stack<ParticleScript>();
            SetupObjectData();
        }
        protected virtual void SetupObjectData()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                ParticleScript tmp;
                tmp = Instantiate(prefab);
                tmp.gameObject.SetActive(false);
                pooledObject.Push(tmp);
            }
        }
        public virtual ParticleScript GetObject()
        {
            ParticleScript data = null;

            if (pooledObject.Count > 0)
            {
                data = pooledObject.Pop();
                if (data != null)
                {
                    data.gameObject.SetActive(true);
                }
            }
            else
            {
                data = Instantiate(prefab);
                if (data != null)
                {
                    data.gameObject.SetActive(true);

                }
                return data;
            }
            return data;
        }
        public virtual void Push(ParticleScript data)
        {
            data.gameObject.SetActive(false);
            pooledObject.Push(data);
        }
    }
}
