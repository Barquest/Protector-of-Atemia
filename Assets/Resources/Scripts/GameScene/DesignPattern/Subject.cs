using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public enum Act {Attack,DamageCaravan,GameOverLose,GameOverWin,Add1KillCount }
    public class Subject : MonoBehaviour
    {
        private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public void RemoveObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        protected void NotifyObservers(Act action)
        {
            observers.ForEach((observer) =>
            {
                observer.OnNotify(action);
            });
        }
    }
}
