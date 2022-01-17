using ML.GameEvents;
using UnityEngine;

namespace ML.GamePlay
{
    public class BombTracker : MonoBehaviour
    {
        [SerializeField] IntEvent onBombsChange;
        [SerializeField] VoidEvent onBombEarn;
        [SerializeField] IntListener onEarnPoint;
        [SerializeField] VoidListener onUseBomb;
        [SerializeField] VoidListener onPlayerLose;
        [SerializeField] GamePlaySettings gamePlaySettings;

        public static int bombs = 0;
        private void Start()
        {
            onEarnPoint.onGameEventInvoke += OnEarnPoint;
            onEarnPoint.HookToGameEvent();
            onUseBomb.onGameEventInvoke += (arg) => { bombs--; onBombsChange?.Invoke(bombs); };
            onUseBomb.HookToGameEvent();
            onPlayerLose.onGameEventInvoke += (arg) => { bombs = 0; onBombsChange?.Invoke(bombs); };
            onPlayerLose.HookToGameEvent();

            onBombsChange?.Invoke(bombs);
        }

        public static bool CanUseBomb() => bombs > 0 ? true : false;

        private void OnEarnPoint(int val)
        {
            if (val == 0)
                return;
            if (bombs >= gamePlaySettings.MaxBombCount)
                return;
            float mod = PointsTracker.points % gamePlaySettings.BombPerObstacles;
            if (mod == 0)
            {
                bombs++;
                onBombsChange?.Invoke(bombs);
                onBombEarn?.Invoke();
            }
        }
    }
}
