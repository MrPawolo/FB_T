using System;
using UnityEngine.Events;

namespace ML.GameEvents
{
    [System.Serializable]
    public class VoidListener : BaseGameEventListener<Void, VoidEvent, Action<Void>>
    {

    }
}
