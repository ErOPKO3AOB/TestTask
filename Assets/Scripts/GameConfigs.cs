using Assets.Scripts.Health.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Game/GameConfigs")]
    public class GameConfigs : ScriptableObject
    {
        [Header("Prefabs")]
        public GameObject playerPrefab;
        public GameObject CM_FollowCamPrefab;
        public List<EnemySettings> enemies;
        public GameObject bulletPrefab;
        public GameObject aim;

        [Header("UI")]
        public GameObject gameCanvas;
        public HealthBarUI healthBarUI;
        public HealthBarHUD healthHUD;

        [Header("Game settings")]
        public float baseSpawnInterval = 2.0f;
        public float minSpawnInterval = 0.15f;
        public float maxSpawnRate = 8f;
        public AnimationCurve spawnRateCurve = AnimationCurve.Linear(0f, 1f, 180f, 6f);
        public float minSpawnRadius = 12f;
        public float maxSpawnRadius = 25f;
        public int maxSpawnAttempts = 12;
        public float navMeshSampleDistance = 2.5f;
        public LayerMask obstacleMask;

        #region Logic
        private void OnValidate()
        {
            if (enemies != null && enemies.Count > 0)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].prefab != null)
                    {
                        enemies[i].name = enemies[i].prefab.gameObject.name;
                    }
                }
            }
        }
        #endregion
    }

    [Serializable]
    public class EnemySettings
    {
        public string name;
        public GameObject prefab;
    }
}
