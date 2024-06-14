using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class PoolData<T>
    {
        public T data { get; set; }
    }
    public class Pool : MonoBehaviour
    {
        [SerializeField] protected int amountToPool = 5;
        [SerializeField] protected GameObject prefab;
        protected Stack<GameObject> pooledObject = new Stack<GameObject>();

        private void Start()
        {
            pooledObject = new Stack<GameObject>();
            SetupObjectData();
        }
        protected virtual void SetupObjectData()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject tmp;
                tmp = Instantiate(prefab);
                tmp.SetActive(false);
                pooledObject.Push(tmp);
            }
        }
        public virtual GameObject GetObject()
        {
            GameObject data = null;

            if (pooledObject.Count > 0)
            {
                data = pooledObject.Pop();
                if (data != null)
                {
                    data.SetActive(true);
                }
            }
            else
            {
                data = Instantiate(prefab);
                if (data != null)
                {
                    data.SetActive(true);

                }
                return data;
            }
            return data;
        }
        public virtual void Push(GameObject data)
        {
            data.gameObject.SetActive(false);
            pooledObject.Push(data);
        }
    }
}