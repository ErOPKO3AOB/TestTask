using Assets.Scripts.Enemy.Services;
using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyModel
    {
        public EnemyModel(EnemyView enemyView, MovementService movementService, HealthService healthService)
        {
            enemyView.Construct(this);
            this.movementService = movementService;
            this.healthService = healthService;
        }

        public readonly MovementService movementService;
        public readonly HealthService healthService;
    }
}
