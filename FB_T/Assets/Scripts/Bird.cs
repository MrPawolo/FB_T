using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;

public class Bird : MonoBehaviour
{
    [SerializeField] GamePlaySettings gamePlaySettings;
    [SerializeField] VoidListener onJump;
    [SerializeField] VoidListener onGameStart;
    [SerializeField] VoidListener onDie;
    [SerializeField] Transform birdModelRoot;

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
    }
    private void EventsUnhooks()
    {
        onJump.onGameEventInvoke -= AddJumpBuffer;
        onJump.UnHookFromGameEvent();
        onGameStart.onGameEventInvoke -= OnGameStart;
        onGameStart.UnHookFromGameEvent();
        onDie.onGameEventInvoke -= OnDie;
        onDie.UnHookFromGameEvent();
    }
    void AddJumpBuffer(Void arg)
    {
        if(!die)
            if(myRigidbody.position.y < gamePlaySettings.MaxAltitudeHeight)
                myRigidbody.velocity = (Vector2.up * gamePlaySettings.JumpVelocity);
    }
    void OnDie(Void arg) => die = true;
    void OnGameStart(Void arg) => die = false;

    private void FixedUpdate()
    {
        float yVel = myRigidbody.velocity.y == 0 ? -6 : myRigidbody.velocity.y;
        birdModelRoot.rotation = Quaternion.AngleAxis(Mathf.Clamp(yVel,-6,6)*gamePlaySettings.BirdRotateMul,Vector3.forward);
        
    }
}
