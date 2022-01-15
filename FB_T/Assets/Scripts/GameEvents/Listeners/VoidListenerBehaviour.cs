using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ML.GameEvents {
    public class VoidListenerBehaviour : MonoBehaviour
    {
        public BaseGameEventListener<Void, VoidEvent, UnityEvent<Void>> gameEvent = new BaseGameEventListener<Void, VoidEvent, UnityEvent<Void>>();
        private void OnEnable()
        {
            gameEvent.OnEnable();
        }
        private void OnDisable()
        {
            gameEvent.OnDisable();
        }
    } }
