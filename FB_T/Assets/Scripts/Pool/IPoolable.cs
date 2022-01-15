using System;
using UnityEngine;

namespace ML.Pool
{
    public interface IPoolable
    {
        public void ActionOnRelease(Action<GameObject> action);
    }
}
