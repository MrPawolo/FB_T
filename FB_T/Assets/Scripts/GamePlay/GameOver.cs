using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;
using System;

public class GameOver : MonoBehaviour
{
    [SerializeField] VoidListener onBirdDie;
    [SerializeField] VoidListener onLevelRestart;
    [SerializeField] VoidEvent onGameOver;


    public static bool gameOver = false;
    private void Start()
    {
        onBirdDie.onGameEventInvoke += (arg) => {
            if (gameOver)
                return;
            gameOver = true;
            onGameOver?.Invoke();
        };
        onBirdDie.HookToGameEvent();


        onLevelRestart.onGameEventInvoke += (arg) => { gameOver = false; };
        onLevelRestart.HookToGameEvent();
    }

}
