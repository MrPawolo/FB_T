using System;
using UnityEngine;

namespace ML.Pool
{
    public interface IPoolable
    {
        //public void ActionOnRelease(Action<GameObject> action);
        public Action<GameObject> onRelease { get; set; }
    }
}
