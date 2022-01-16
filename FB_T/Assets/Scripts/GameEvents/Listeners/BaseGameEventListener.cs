using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ML.GameEvents
{
    [System.Serializable]
    public class BaseGameEventListener<TParam, TGameEvent, TAction> :
        IGameEventListener<TParam> where TGameEvent : BaseGameEvent<TParam>// where TAction : Action<TParam>
    {
        [SerializeField] TGameEvent gameEvent;
        public TGameEvent GameEvent { get { return gameEvent; } set { gameEvent = value; } }
        public event Action<TParam> onGameEventInvoke;

        public void HookToGameEvent()
        {
            if (gameEvent == null)
                return;
            gameEvent.AddListener(this);
        }
        public void UnHookFromGameEvent()
        {
            if (gameEvent == null)
                return;
            gameEvent.RemoveListener(this);
        }
        public void OnEventInvoke(TParam param)
        {
           onGameEventInvoke?.Invoke(param);
        }
    }
}
