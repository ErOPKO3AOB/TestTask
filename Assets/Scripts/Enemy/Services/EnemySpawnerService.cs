using UnityEngine;
using UnityEngine.AI;
using VContainer.Unity;

namespace Assets.Scripts.Enemy.Services
{
    public class EnemySpawnerService : ITickable, IStartable
    {
        public EnemySpawnerService(EnemyFactory enemyFactory, GameConfigs gameConfigs)
        {
            _enemyFactory = enemyFactory;
            _gameConfigs = gameConfigs;
        }

        private readonly EnemyFactory _enemyFactory;
        private readonly GameConfigs _gameConfigs;

        private float _elapsedTime;
        private float _spawnTimer;

        public void Start()
        {
            _elapsedTime = 0f;
            _spawnTimer = 0f;
        }

        public void Tick()
        {
            float deltaTime = Time.deltaTime;
            _elapsedTime += deltaTime;
            _spawnTimer += deltaTime;

            float currentInterval = GetCurrentSpawnInterval();

            while (_spawnTimer >= currentInterval)
            {
                _spawnTimer -= currentInterval;
                TrySpawnEnemy();
                currentInterval = GetCurrentSpawnInterval();
            }
        }

        private float GetCurrentSpawnInterval()
        {
            float rateMultiplier = Mathf.Max(0.01f, _gameConfigs.spawnRateCurve.Evaluate(_elapsedTime));
            float interval = _gameConfigs.baseSpawnInterval / rateMultiplier;

            interval = Mathf.Max(interval, _gameConfigs.minSpawnInterval);
            interval = Mathf.Max(interval, 1f / _gameConfigs.maxSpawnRate);

            return interval;
        }

        private void TrySpawnEnemy()
        {
            Vector3 center = Vector3.zero;

            if (TryGetSpawnPosition(center, out Vector3 spawnPos))
            {
                string enemyName = _gameConfigs.enemies[0].name;
                _enemyFactory.Create(enemyName, spawnPos);
            }
            // если не нашли позицию — просто пропускаем этот спавн
        }

        private bool TryGetSpawnPosition(Vector3 center, out Vector3 result)
        {
            for (int i = 0; i < _gameConfigs.maxSpawnAttempts; i++)
            {
                // Случайная точка в кольце (равномерное распределение по площади)
                float angle = Random.Range(0f, Mathf.PI * 2f);
                float radius = Mathf.Sqrt(Random.Range(_gameConfigs.minSpawnRadius * _gameConfigs.minSpawnRadius,
                                                       _gameConfigs.maxSpawnRadius * _gameConfigs.maxSpawnRadius));

                Vector3 candidate = center + new Vector3(
                    Mathf.Cos(angle) * radius,
                    0f,
                    Mathf.Sin(angle) * radius
                );

                // 1. Проверка NavMesh
                if (!NavMesh.SamplePosition(candidate, out NavMeshHit navHit, _gameConfigs.navMeshSampleDistance, NavMesh.AllAreas))
                    continue;

                // 2. Проверка препятствий (сфера)
                if (Physics.CheckSphere(navHit.position + Vector3.up * 0.5f, 0.6f, _gameConfigs.obstacleMask))
                    continue;

                result = navHit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }

        public void Reset()
        {
            _elapsedTime = 0f;
            _spawnTimer = 0f;
        }
    }
}