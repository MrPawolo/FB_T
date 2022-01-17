using ML.GameEvents;
using ML.Pool;
using UnityEngine;

namespace ML.GamePlay
{
    public class Obstacle : MonoBehaviour, IPoolable, IDestroy
    {
        [SerializeField] GamePlaySettings gamePlaySettings;
        [SerializeField] VoidListener onBirdDie;
        [SerializeField] VoidListener onGameStart;

        float speedMul = 1;
        Rigidbody2D myRigidbody;

        public System.Action<GameObject> onRelease { get; set; }

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

        private void FixedUpdate()
        {
            myRigidbody.MovePosition(myRigidbody.position + Vector2.left * gamePlaySettings.Speed * Time.fixedDeltaTime * speedMul);
        }

        public void Destroy()
        {
            onRelease?.Invoke(this.gameObject);
        }
    }
}
