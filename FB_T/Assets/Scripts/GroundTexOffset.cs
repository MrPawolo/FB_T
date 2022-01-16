using ML.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTexOffset : MonoBehaviour
{
    [SerializeField] GamePlaySettings gamePlaySettings;

    [SerializeField] Material mat;
    float offset = 0;

    [SerializeField] VoidListener onBirdDie;
    [SerializeField] VoidListener onGameStart;
    float speedMul = 1;

    Rigidbody2D myRigidbody;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        onBirdDie.onGameEventInvoke += OnBirdDie;
        onBirdDie.HookToGameEvent();
        onGameStart.onGameEventInvoke += OnGameStart;
        onGameStart.HookToGameEvent();
    }
    private void OnDestroy()
    {
        onBirdDie.onGameEventInvoke -= OnBirdDie;
        onBirdDie.UnHookFromGameEvent();
        onGameStart.onGameEventInvoke -= OnGameStart;
        onGameStart.UnHookFromGameEvent();
    }
    void OnBirdDie(Void arg) => speedMul = 0;
    void OnGameStart(Void arg) => speedMul = 1;
    void FixedUpdate()
    {
        if (speedMul == 1)
        {
            offset += gamePlaySettings.Speed * Time.deltaTime;
            mat.SetTextureOffset("_MainTex", Vector2.right * offset / transform.localScale.x);
        }
    }
}
