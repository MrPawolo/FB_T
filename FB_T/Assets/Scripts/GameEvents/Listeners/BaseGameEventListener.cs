using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ML.GameEvents
{
    [System.Serializable]
    public class BaseGameEventListener<TParam, TGameEvent, TUnityEvent> :
        IGameEventListener<TParam> where TGameEvent : BaseGameEvent<TParam> where TUnityEvent : UnityEvent<TParam>
    {
        [SerializeField] TGameEvent gameEvent;
        public TGameEvent GameEvent { get { return gameEvent; } set { gameEvent = value; } }
        [SerializeField] TUnityEvent unityEvent;
        public TUnityEvent UnityEvent { get { return unityEvent;  }  set { unityEvent = value; } }

        public void OnEnable()
        {
            if (gameEvent == null)
                return;
            gameEvent.AddListener(this);
        }
        public void OnDisable()
        {
            if (gameEvent == null)
                return;
            gameEvent.RemoveListener(this);
        }
        public void AddListener(UnityAction<TParam> action)
        {
            if (unityEvent == null)
                return;
            unityEvent.AddListener(action);
            gameEvent.AddListener(this);
        }
        public void RemoveListener(UnityAction<TParam> action)
        {
            if (unityEvent == null)
                return;
            unityEvent.RemoveListener(action);
            gameEvent.RemoveListener(this);
        }
        public void OnEventInvoke(TParam param)
        {
            unityEvent?.Invoke(param);
        }
    }
}
