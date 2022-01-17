using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.GamePlay
{
    [CreateAssetMenu(fileName = "GamePlaySettings", menuName = "ML/GamePlaySettings")]
    public class GamePlaySettings : ScriptableObject
    {
        [Header("Bombs")]
        [SerializeField] int maxBombCount = 3;
        [SerializeField] int bombPerObstacles = 10;
        [SerializeField] float doubleTapTime = 0.1f;
        [Header("Bird")]
        [SerializeField] float maxAltitudeHeight = 3.3f;
        [SerializeField] float jumpVelocity = 5.5f;
        [SerializeField] float birdRoateMul = 10f;
        [Header("Obstacle Settings")]
        [SerializeField] float gapSize = 2f;
        [SerializeField] float speed = 2f;
        [SerializeField] float spawnRate = 1f;
        [SerializeField] Vector2 minMaxObstacleGapHeight = new Vector2(-2, 2);

        public int MaxBombCount { get { return maxBombCount; } }
        public int BombPerObstacles { get { return bombPerObstacles; } }
        public float DoubleTapTime { get { return doubleTapTime; } }
        public float MaxAltitudeHeight { get { return maxAltitudeHeight; } }
        public float JumpVelocity { get { return jumpVelocity; } }
        public float BirdRotateMul { get { return birdRoateMul; } }
        public float Speed { get { return speed; } }
        public float GapSize { get { return gapSize; } }
        public float SpawnRate { get { return spawnRate; } }
        public Vector2 MinMaxObstacleGapHeight { get { return minMaxObstacleGapHeight; } }
    }
}
