using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ML.GameEvents
{
    public class EventHelpers
    {
        public static void HookEvent<TParam, TGameEvent, TUnityEvent>(
            BaseGameEvent<TParam> gameEvent,
            BaseGameEventListener<TParam, TGameEvent,TUnityEvent> listener,
            UnityAction<TParam> action)
            where TGameEvent : BaseGameEvent<TParam> 
            where TUnityEvent : UnityEvent<TParam>
        {
            if(gameEvent != null)
            {
                listener.AddListener(action);
                //gameEvent.AddListener(listener);
            }
        }

        public static void UnHookEvent<TParam, TGameEvent, TUnityEvent>(
            BaseGameEvent<TParam> gameEvent,
            BaseGameEventListener<TParam, TGameEvent, TUnityEvent> listener,
            UnityAction<TParam> action)
            where TGameEvent : BaseGameEvent<TParam>
            where TUnityEvent : UnityEvent<TParam>
        {
            if (gameEvent != null)
            {
                listener.RemoveListener(action);
                //gameEvent.AddListener(listener);
            }
        }
    }
}