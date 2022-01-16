using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ML.GameEvents {
    public class VoidListenerBehaviour : MonoBehaviour
    {
        public VoidListener gameEvent = new VoidListener();
        public UnityEvent onEventInvoke;
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

        void OnEvent(Void param)
        {
            onEventInvoke?.Invoke();
        }
    } }
