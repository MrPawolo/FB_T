using ML.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GamePlaySettings gamePlaySettings;
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
    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.position += Vector3.up *
            Random.Range(gamePlaySettings.MinMaxObstacleGapHeight.x, gamePlaySettings.MinMaxObstacleGapHeight.x);
    }

    void OnBirdDie(Void arg) => speedMul = 0;
    void OnGameStart(Void arg) => speedMul = 1;
    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + Vector2.left * gamePlaySettings.Speed * Time.fixedDeltaTime * speedMul);
    }
}
