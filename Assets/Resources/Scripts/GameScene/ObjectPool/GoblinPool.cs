using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class GoblinPool : MonoBehaviour
    {
        [SerializeField] protected int amountToPool = 5;
        [SerializeField] protected Enemy prefab;
        protected Stack<Enemy> pooledObject = new Stack<Enemy>();

        private void Start()
        {
            pooledObject = new Stack<Enemy>();
            SetupObjectData();
        }
        protected virtual void SetupObjectData()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                Enemy tmp;
                tmp = Instantiate(prefab);
                tmp.gameObject.SetActive(false);
                pooledObject.Push(tmp);
            }
        }
        public virtual Enemy GetObject()
        {
            Enemy data = null;

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
        public virtual void Push(Enemy data)
        {
            data.gameObject.SetActive(false);
            pooledObject.Push(data);
        }
    }
}
