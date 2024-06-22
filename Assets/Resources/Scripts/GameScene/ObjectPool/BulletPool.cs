using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] protected int amountToPool = 5;
        [SerializeField] protected Bullet prefab;
        protected Stack<Bullet> pooledObject = new Stack<Bullet>();

        private void Start()
        {
            pooledObject = new Stack<Bullet>();
            SetupObjectData();
        }
        protected virtual void SetupObjectData()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                Bullet tmp;
                tmp = Instantiate(prefab);
                tmp.gameObject.SetActive(false);
                pooledObject.Push(tmp);
            }
        }
        public virtual Bullet GetObject()
        {
            Bullet data = null;

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
        public virtual void Push(Bullet data)
        {
            data.gameObject.SetActive(false);
            pooledObject.Push(data);
        }
    }
}

