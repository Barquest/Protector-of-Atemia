using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class EventDatabase : MonoBehaviour
    {
        public static EventDatabase Instance;

        [SerializeField] private List<EventData> eventData = new List<EventData>();

        [SerializeField] private Dictionary<string, EventData> eventDictionary = new Dictionary<string, EventData>();

        public EventData GetEventData(string eventName)
        {
            return eventDictionary[eventName];
        }
        private void Awake()
        {
            Initialize();
        }
        private void Initialize()
        {
            for (int i = 0; i < eventData.Count; i++)
            {
                eventDictionary[eventData[i].name] = eventData[i];
            }
        }
    }
}
