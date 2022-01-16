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
    [SerializeField] VoidListener onDie;
    [SerializeField] VoidListener onLevelRestart;
    [SerializeField] Transform birdModelRoot;

    [SerializeField] UnityEvent onJumpEvent;

    bool die = false;
    Rigidbody2D myRigidbody;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        EventsHooks();
    }
    private void OnDestroy()
    {
        EventsUnhooks();
    }
    private void EventsHooks()
    {
        onJump.onGameEventInvoke += AddJumpBuffer;
        onJump.HookToGameEvent();
        onGameStart.onGameEventInvoke += OnGameStart;
        onGameStart.HookToGameEvent();
        onDie.onGameEventInvoke += OnDie;
        onDie.HookToGameEvent();
        onLevelRestart.onGameEventInvoke += OnGameRestart;
        onLevelRestart.HookToGameEvent();
    }
    private void EventsUnhooks()
    {
        onJump.onGameEventInvoke -= AddJumpBuffer;
        onJump.UnHookFromGameEvent();
        onGameStart.onGameEventInvoke -= OnGameStart;
        onGameStart.UnHookFromGameEvent();
        onDie.onGameEventInvoke -= OnDie;
        onDie.UnHookFromGameEvent();
        onLevelRestart.onGameEventInvoke -= OnGameRestart;
        onLevelRestart.UnHookFromGameEvent();
    }
    void AddJumpBuffer(Void arg)
    {
        if (!die)
            if (myRigidbody.position.y < gamePlaySettings.MaxAltitudeHeight)
            {
                myRigidbody.velocity = (Vector2.up * gamePlaySettings.JumpVelocity);
                onJumpEvent?.Invoke();
            }
    }
    void OnDie(Void arg) => die = true;
    void OnGameStart(Void arg)
    {
        die = false;
        myRigidbody.gravityScale = 1;
    }
    void OnGameRestart(Void arg)
    {
        myRigidbody.gravityScale = 0;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    private void FixedUpdate()
    {
        float yVel = myRigidbody.velocity.y == 0 ? -6 : myRigidbody.velocity.y;
        if (myRigidbody.gravityScale == 0) yVel = 0;
        birdModelRoot.rotation = Quaternion.AngleAxis(Mathf.Clamp(yVel,-6,6)*gamePlaySettings.BirdRotateMul,Vector3.forward);
        
    }
}
