using ML.GameEvents;
using ML.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPoolable, IDestroy
{
    [SerializeField] GamePlaySettings gamePlaySettings;
    [SerializeField] VoidListener onBirdDie;
    [SerializeField] VoidListener onGameStart;
    float speedMul = 1;
    
    Rigidbody2D myRigidbody;

    public System.Action<GameObject> onRelease { get ; set ; }

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
        
    }

    void OnBirdDie(Void arg) => speedMul = 0;
    void OnGameStart(Void arg) => speedMul = 1;
    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + Vector2.left * gamePlaySettings.Speed * Time.fixedDeltaTime * speedMul);
    }

    [ContextMenu("ForceDestroy")]
    public void Destroy()
    {
        onRelease?.Invoke(this.gameObject);
    }
}
