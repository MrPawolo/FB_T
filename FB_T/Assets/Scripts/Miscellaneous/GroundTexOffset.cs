using ML.GameEvents;
using ML.GamePlay;
using UnityEngine;

public class GroundTexOffset : MonoBehaviour
{
    [SerializeField] GamePlaySettings gamePlaySettings;

    [SerializeField] Material mat;
    float offset = 0;

    [SerializeField] VoidListener onBirdDie;
    [SerializeField] VoidListener onGameStart;
    float speedMul = 0;

    Rigidbody2D myRigidbody;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        onBirdDie.onGameEventInvoke += OnBirdDie;
        onBirdDie.HookToGameEvent();
        onGameStart.onGameEventInvoke += OnGameStart;
        onGameStart.HookToGameEvent();
        mat.SetTextureOffset("_MainTex", Vector2.zero);
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
