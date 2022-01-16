using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ML.GameEvents;
using System;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] IntListener onPointUpdate;
    [SerializeField] VoidListener onGameOver;
    [SerializeField] GameObject textPrefab;
    [SerializeField] VoidEvent onNewHighScore;
    List<TMP_Text> texts = new List<TMP_Text>();
    List<int> hightScores = new List<int>();
    int scoresToShow = 5;
    int points = 0;

    private void Awake() => Load();
    private void Load()
    {
        for (int i = 0; i < scoresToShow; i++)
        {
            hightScores.Add(0);
        }
    }

    void Start()
    {
        onPointUpdate.onGameEventInvoke += OnPointUpdate;
        onPointUpdate.HookToGameEvent();
        onGameOver.onGameEventInvoke += OnGameOver;
        onGameOver.HookToGameEvent();

        for(int i = 0; i < scoresToShow; i++)
        {
            GameObject gameObject = Instantiate(textPrefab);
            gameObject.transform.SetParent(transform);
            gameObject.transform.localScale = Vector3.one;
            texts.Add(gameObject.GetComponent<TMP_Text>());
        }
    }
    private void OnDestroy()
    {
        onPointUpdate.onGameEventInvoke -= OnPointUpdate;
        onPointUpdate.UnHookFromGameEvent();
        onGameOver.onGameEventInvoke -= OnGameOver;
        onGameOver.UnHookFromGameEvent();
    }

    private void OnGameOver(ML.GameEvents.Void obj)
    {
        CheckForNewHighScore();
        DrawLeaderBoard();
    }

    private void CheckForNewHighScore()
    {
        int minI = int.MinValue;
        for (int i = 0; i < hightScores.Count; i++)
        {
            if (minI < hightScores[i])
                minI = hightScores[i];
        }
        if (points > minI)
        {
            onNewHighScore?.Invoke();
        }
    }

    private void DrawLeaderBoard()
    {
        hightScores.Add(points);
        hightScores.Sort();
        if(hightScores.Count > scoresToShow)
        {
            hightScores.RemoveAt(0);
        }
        hightScores.Reverse();
        for(int i = 0; i < hightScores.Count; i++)
        {
            texts[i].text = $"{i+1}. {hightScores[i]}"; 
        }
    }

    private void OnPointUpdate(int val) 
    {
        points = val;
    }
}
