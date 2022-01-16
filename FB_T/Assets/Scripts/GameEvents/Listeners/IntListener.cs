using System;
using UnityEngine.Events;

namespace ML.GameEvents
{
    [System.Serializable]
    public class IntListener : BaseGameEventListener<int, IntEvent, Action<int>>
    {

    }
}
