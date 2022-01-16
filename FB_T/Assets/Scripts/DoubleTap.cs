using ML.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTap : MonoBehaviour
{
    [SerializeField] VoidEvent onDoubleTap;
    [SerializeField] GamePlaySettings gamePlaySettings;
    [SerializeField] VoidListener onTap;
    double lastTimeClick = 0;

    private void Awake()
    {
        onTap.onGameEventInvoke += OnTap;
    }
    private void OnEnable()
    {
        onTap.HookToGameEvent();
    }
    private void OnDisable()
    {
        onTap.UnHookFromGameEvent();   
    }
    private void OnDestroy()
    {
        onTap.onGameEventInvoke -= OnTap;
    }
    void OnTap(Void arg)
    {
        double deltaTime = Time.realtimeSinceStartupAsDouble - lastTimeClick;
        if (deltaTime < gamePlaySettings.DoubleTapTime)
        {
            onDoubleTap?.Invoke();
        }
        lastTimeClick = Time.realtimeSinceStartupAsDouble;
    }
}
