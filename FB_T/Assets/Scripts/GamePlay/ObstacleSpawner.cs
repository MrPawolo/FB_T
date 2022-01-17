using UnityEngine;
using ML.Pool;
using ML.GameEvents;
using System.Collections;

namespace ML.GamePlay
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] int maxObstacles = 6;
        [SerializeField] int defaultObstacles = 6;
        [SerializeField] GamePlaySettings gamePlaySettings;
        [SerializeField] GameObject obstaclePrefab;

        [SerializeField] VoidListener onGameStart;
        [SerializeField] VoidListener onGameOver;
        [SerializeField] VoidListener onLevelRestart;

        GenericPool<GameObject> obstaclePool;
        Coroutine spawnCorutine;

        void Awake()
        {
            obstaclePool = new GenericPool<GameObject>(funcOnCreate, actionOnGet, actionOnRelease,
                actionOnDestroy, defaultObstacles, maxObstacles);
        }
        private void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            HookEvents();
        }

        void HookEvents()
        {
            onGameStart.onGameEventInvoke += StartSpawning;
            onGameStart.HookToGameEvent();
            onGameOver.onGameEventInvoke += StopSpawning;
            onGameOver.HookToGameEvent();
            onLevelRestart.onGameEventInvoke += ClearObstacles;
            onLevelRestart.HookToGameEvent();
        }

        private void OnDestroy()
        {
            UnHookEvents();
        }

        void UnHookEvents()
        {
            onGameStart.onGameEventInvoke -= StartSpawning;
            onGameStart.UnHookFromGameEvent();
            onGameOver.onGameEventInvoke -= StopSpawning;
            onGameOver.UnHookFromGameEvent();
            onLevelRestart.onGameEventInvoke -= ClearObstacles;
            onLevelRestart.UnHookFromGameEvent();
        }

        private void actionOnDestroy(GameObject obj)
        {
            Destroy(obj);
        }

        private void actionOnRelease(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void actionOnGet(GameObject obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = transform.position;
            obj.transform.SetParent(transform);
            float num = Random.Range(gamePlaySettings.MinMaxObstacleGapHeight.x, gamePlaySettings.MinMaxObstacleGapHeight.y);
            obj.transform.position += Vector3.up * num;
        }

        private GameObject funcOnCreate()
        {
            GameObject gameObject = Instantiate(obstaclePrefab);
            gameObject.SetActive(false);
            gameObject.transform.SetParent(transform);
            if (gameObject.TryGetComponent(out IPoolable poolable))
            {
                poolable.onRelease = actionOnRelease;
            }
            return gameObject;
        }

        void StartSpawning(Void arg)
        {
            if (spawnCorutine == null)
                spawnCorutine = StartCoroutine(SpawnCorutine());
        }

        void StopSpawning(Void arg)
        {
            if (spawnCorutine != null)
            {
                StopCoroutine(spawnCorutine);
                spawnCorutine = null;
            }
        }

        IEnumerator SpawnCorutine()
        {
            while (true)
            {
                obstaclePool.Get();
                yield return new WaitForSeconds(gamePlaySettings.SpawnRate);
            }
        }

        void ClearObstacles(Void arg)
        {
            obstaclePool.Dispose();
        }
    }
}
