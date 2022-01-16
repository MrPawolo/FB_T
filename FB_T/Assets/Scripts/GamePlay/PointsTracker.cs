using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;

public class PointsTracker : MonoBehaviour
{
    [SerializeField] GamePlaySettings gamePlaySettings;
    [SerializeField] IntEvent onUpdatePoints;
    [SerializeField] VoidListener onEarnPoint;
    [SerializeField] VoidListener onGameStart;
    [SerializeField] VoidListener onGameOver;
    [SerializeField] VoidEvent onEarnBomb;
 
    int points = 0;
    bool isDead = false;

    private void Start()
    {
        onEarnPoint.onGameEventInvoke += OnEarnPoint;
        onEarnPoint.HookToGameEvent();
        onGameStart.onGameEventInvoke += OnGameStart;
        onGameStart.HookToGameEvent();
        onGameOver.onGameEventInvoke += OnGameOver;
        onGameOver.HookToGameEvent();
    }
    private void OnDisable()
    {
        onEarnPoint.onGameEventInvoke -= OnEarnPoint;
        onEarnPoint.UnHookFromGameEvent();
        onGameStart.onGameEventInvoke -= OnGameStart;
        onGameStart.UnHookFromGameEvent();
        onGameOver.onGameEventInvoke -= OnGameOver;
        onGameOver.UnHookFromGameEvent();
    }

    void OnEarnPoint(Void arg)
    {
        if (isDead)
            return;
        points++;
        onUpdatePoints?.Invoke(points);
        float resoult = points / gamePlaySettings.BombPerObstacles;
        if ( (Mathf.Floor(resoult) - resoult) == 0)
        {
            onEarnBomb?.Invoke();
        }
    }

    void OnGameOver(Void arg)
    {
        if (isDead)
            return;
        isDead = true;
        onUpdatePoints?.Invoke(points);
    }

    void OnGameStart(Void arg)
    {
        isDead = false;
        points = 0;
        onUpdatePoints?.Invoke(points);
    }


}
