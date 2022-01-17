using UnityEngine;
using ML.GameEvents;

namespace ML.GamePlay
{
    public class PointsTracker : MonoBehaviour
    {
        [SerializeField] GamePlaySettings gamePlaySettings;
        [SerializeField] IntEvent onUpdatePoints;
        [SerializeField] VoidListener onEarnPoint;
        [SerializeField] VoidListener onGameStart;
        [SerializeField] VoidListener onLevelRestart;

        public static int points = 0;

        private void Start()
        {
            onEarnPoint.onGameEventInvoke += OnEarnPoint;
            onEarnPoint.HookToGameEvent();
            onGameStart.onGameEventInvoke += OnGameStart;
            onGameStart.HookToGameEvent();
            onLevelRestart.onGameEventInvoke += OnLevelRestart;
            onLevelRestart.HookToGameEvent();
        }

        private void OnDisable()
        {
            onEarnPoint.onGameEventInvoke -= OnEarnPoint;
            onEarnPoint.UnHookFromGameEvent();
            onGameStart.onGameEventInvoke -= OnGameStart;
            onGameStart.UnHookFromGameEvent();
            onLevelRestart.onGameEventInvoke -= OnLevelRestart;
            onLevelRestart.UnHookFromGameEvent();
        }

        private void OnLevelRestart(Void obj)
        {
            points = 0;
        }

        void OnEarnPoint(Void arg)
        {
            if (GameOver.gameOver)
                return;
            points++;
            onUpdatePoints?.Invoke(points);
        }

        void OnGameStart(Void arg)
        {
            points = 0;
        }
    }
}
