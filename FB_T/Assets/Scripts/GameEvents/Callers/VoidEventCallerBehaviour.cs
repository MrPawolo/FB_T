using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.GameEvents
{
    public class VoidEventCallerBehaviour : MonoBehaviour
    {
        [SerializeField]VoidEvent gameEvent;

        public VoidEvent GameEvent {  get { return gameEvent; } set { gameEvent = value; } }

        public void Invoke()
        {
            gameEvent.Invoke();
        }
    }
}
