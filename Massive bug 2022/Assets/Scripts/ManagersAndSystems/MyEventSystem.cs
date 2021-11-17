using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDict
{
    public class MyEventSystem 
    {
        private static MyEventSystem _instance;
        public static MyEventSystem instance
        {
            get
            {
                if (_instance == null) _instance = GetEventSystemObj();
                return _instance;
            }
        }
        private static MyEventSystem GetEventSystemObj()
        {

            /* MyEventSystem eventSystem = FindObjectOfType<MyEventSystem>();
             if (eventSystem != null) return eventSystem;

             GameObject obj = new GameObject("My Event System");
             return obj.AddComponent<MyEventSystem>();*/
            return new MyEventSystem();
        }
        public class ActionEvent
        {
            public event Action OnEventOccur;
            public void InvokeEvent()
            {
                if (OnEventOccur != null)
                {                    
                    OnEventOccur.Invoke();
                }
            }
        }
        public class IntEvent
        {
            public event Action<int> OnEventOccur;
            public void InvokeEvent(int value)
            {
                OnEventOccur?.Invoke(value);
            }
        }
        //Update when day passed, used to make change to stats
        public ActionEvent DayPassedEvent = new ActionEvent();

        //Late update when day passed, used to read stats
        public ActionEvent DayPassedLateEvent = new ActionEvent();  

        public ActionEvent NoMoneyLeft = new ActionEvent();

        public ActionEvent BugDie = new ActionEvent();

        public IntEvent IncreaseExp = new IntEvent();
    }
}
