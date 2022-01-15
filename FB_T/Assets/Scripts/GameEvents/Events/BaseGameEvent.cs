using System.Collections.Generic;
using UnityEngine;

namespace ML.GameEvents {
    public class BaseGameEvent<TParam> : ScriptableObject
    {
        List<IGameEventListener<TParam>> eventListeners = new List<IGameEventListener<TParam>>();

        public void Invoke(TParam param)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventInvoke(param);
            }
        }

        public void AddListener(IGameEventListener<TParam> listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }
        public void RemoveListener(IGameEventListener<TParam> listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    } 
}
