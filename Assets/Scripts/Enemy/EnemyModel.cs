using Assets.Scripts.Enemy.Services;
using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyModel
    {
        public EnemyModel(MovementService movementService, HealthService healthService)
        {
            _movementService = movementService;
            _healthService = healthService;
        }

        private readonly MovementService _movementService;
        private readonly HealthService _healthService;
    }
}
