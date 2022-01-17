using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ML.GameEvents;
using ML.GamePlay;
using ML.SaveSystem;

namespace ML.UI
{
    public class LeaderBoard : MonoBehaviour, ISaveable
    {
        [SerializeField] RectTransform leaderBoardTransform;
        [SerializeField] GameObject textPrefab;
        [SerializeField] VoidListener onGameOver;
        [SerializeField] VoidEvent onNewHighScore;


        List<TMP_Text> texts = new List<TMP_Text>();
        List<int> hightScores = new List<int>();
        readonly int scoresToShow = 5;

        private void Awake() => Load();
        private void Load()
        {
            for (int i = 0; i < scoresToShow; i++)
            {
                hightScores.Add(0);
            }
            SavingLoading.Instance.Load();
        }

        void Start()
        {
            onGameOver.onGameEventInvoke += OnGameOver;
            onGameOver.HookToGameEvent();

            PrewarmLeaderBoard();
        }

        private void PrewarmLeaderBoard()
        {
            for (int i = 0; i < scoresToShow; i++)
            {
                GameObject gameObject = Instantiate(textPrefab);
                gameObject.transform.SetParent(leaderBoardTransform);
                gameObject.transform.localScale = Vector3.one;
                texts.Add(gameObject.GetComponent<TMP_Text>());
            }
        }

        private void OnDestroy()
        {
            onGameOver.onGameEventInvoke -= OnGameOver;
            onGameOver.UnHookFromGameEvent();
        }

        private void OnGameOver(ML.GameEvents.Void obj)
        {
            CheckForNewHighScore();
            DrawLeaderBoard();
        }

        private void CheckForNewHighScore()
        {
            int minI = int.MinValue;
            for (int i = 0; i < hightScores.Count; i++)
            {
                if (minI < hightScores[i])
                    minI = hightScores[i];
            }
            if (PointsTracker.points > minI)
            {
                onNewHighScore?.Invoke();
            }
        }

        private void DrawLeaderBoard()
        {
            if (IsInTheLeaderboardTheSameScore())
                return;
            UpdateHighScores();

            SavingLoading.Instance.Save();
        }

        private void UpdateHighScores()
        {
            hightScores.Add(PointsTracker.points);
            hightScores.Sort();
            if (hightScores.Count > scoresToShow)
            {
                hightScores.RemoveAt(0);
            }
            hightScores.Reverse();

            for (int i = 0; i < hightScores.Count; i++)
            {
                texts[i].text = $"{i + 1}. {hightScores[i]}";
            }
        }

        bool IsInTheLeaderboardTheSameScore()
        {
            for (int i = 0; i < hightScores.Count; i++)
            {
                if (hightScores[i] == PointsTracker.points)
                    return true;
            }
            return false;
        }

        public object CaptureState()
        {
            LeaderBoardState leaderBoardState = new LeaderBoardState();
            leaderBoardState.highScores = hightScores;
            return leaderBoardState;
        }
        public void RestoreState(object state)
        {
            LeaderBoardState leaderBoardState = (LeaderBoardState)state;
            hightScores = leaderBoardState.highScores;
        }
    }
    [System.Serializable]
    public struct LeaderBoardState
    {
        public List<int> highScores;
    }
}
