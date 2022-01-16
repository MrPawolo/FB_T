using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ML.GameEvents {
    public class IntListenerBehaviour : MonoBehaviour
    {
        public IntListener gameEvent = new IntListener();
        public UnityEvent<int> onEventInvoke;
        private void OnEnable()
        {
            gameEvent.onGameEventInvoke += OnEvent;
            gameEvent.HookToGameEvent();
        }
        private void OnDisable()
        {
            gameEvent.onGameEventInvoke -= OnEvent;
            gameEvent.UnHookFromGameEvent();
        }

        void OnEvent(int param)
        {
            onEventInvoke?.Invoke(param);
        }
    } }
