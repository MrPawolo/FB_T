using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    [SerializeField] GamePlaySettings gamePlaySettings;
    [SerializeField] VoidListener onJump;
    [SerializeField] VoidListener onGameStart;
    [SerializeField] VoidListener onLevelRestart;
    [SerializeField] Transform birdModelRoot;

    [SerializeField] UnityEvent onJumpEvent;

    Vector3 startPosition = Vector3.zero;
    Rigidbody2D myRigidbody;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        EventsHooks();
    }
    private void OnDestroy()
    {
        EventsUnhooks();
    }
    private void EventsHooks()
    {
        onJump.onGameEventInvoke += OnJump;
        onJump.HookToGameEvent();
        onGameStart.onGameEventInvoke += OnGameStart;
        onGameStart.HookToGameEvent();
        onLevelRestart.onGameEventInvoke += OnGameRestart;
        onLevelRestart.HookToGameEvent();
    }
    private void EventsUnhooks()
    {
        onJump.onGameEventInvoke -= OnJump;
        onJump.UnHookFromGameEvent();
        onGameStart.onGameEventInvoke -= OnGameStart;
        onGameStart.UnHookFromGameEvent();
        onLevelRestart.onGameEventInvoke -= OnGameRestart;
        onLevelRestart.UnHookFromGameEvent();
    }
    void OnJump(Void arg)
    {
        if (!GameOver.gameOver)
            if (myRigidbody.position.y < gamePlaySettings.MaxAltitudeHeight)
            {
                myRigidbody.velocity = (Vector2.up * gamePlaySettings.JumpVelocity);
                onJumpEvent?.Invoke();
            }
    }
    void OnGameStart(Void arg)
    {
        myRigidbody.gravityScale = 1;
    }
    void OnGameRestart(Void arg)
    {
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = Vector2.zero;
        transform.position = startPosition;
    }
    private void FixedUpdate()
    {
        RotateBird();
    }

    private void RotateBird()
    {
        float yVel = myRigidbody.velocity.y == 0 ? -6 : myRigidbody.velocity.y;
        if (myRigidbody.gravityScale == 0) yVel = 0;
        birdModelRoot.rotation = Quaternion.AngleAxis(Mathf.Clamp(yVel, -6, 6) * gamePlaySettings.BirdRotateMul, Vector3.forward);
    }
}
